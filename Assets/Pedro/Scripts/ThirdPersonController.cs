using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Obligar que el gameObject tenga un CharacterController
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float maximunSpeed;
    [SerializeField] private float gravityScale;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float rotationSmoothFactor;
    [SerializeField] private float movementSmoothFactor;

    [Header("Ground Detection")]
    [SerializeField] private Transform feet;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;

    public event Action<float> OnUpdateMovement;

    private Camera cam;
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Vector2 movementInput;
    private Vector3 verticalMovement; // Sirve para gravedad y para saltar
    private Vector3 lastMovementDirection; // Última dirección en la que ibas
    private float currentSpeed;
    private float currentMovementVelocity;
    private float currentRotationVelocity;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();  
        characterController = GetComponent<CharacterController>(); 
        cam = Camera.main; // Busca en la escen la "MainCamera"
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        playerInput.actions["Move"].performed += UpdateMovement;  
        playerInput.actions["Move"].canceled += UpdateMovement;
        playerInput.actions["Jump"].started += Jump;      
    }

    private void Jump(InputAction.CallbackContext context)
    {
        verticalMovement.y = Mathf.Sqrt(-2 * gravityScale * jumpForce);
    }

    private void UpdateMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        MoveAndRotate();
        
    }

    private void MoveAndRotate() 
    {
        // Si existe input...
        if (movementInput.sqrMagnitude > 0)
        {
            // Se calcula el ángulo en base a los inputs + la rotación de la cámara
            float angleToRotate = Mathf.Atan2(movementInput.x, movementInput.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angleToRotate, ref currentRotationVelocity, 
            rotationSmoothFactor);

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            // Se rota el vector 0, 0, 1 al ángulo calculado en el paso anterior
            Vector3 movementVector = Quaternion.Euler(0, angleToRotate, 0) * Vector3.forward;

            // Actualizo el último movimiento que hubo
            lastMovementDirection = movementInput;

            currentSpeed = Mathf.SmoothDamp(currentSpeed, maximunSpeed, ref currentMovementVelocity, movementSmoothFactor);

            // Moviemiento personaje en X Z
            characterController.Move(currentSpeed * Time.deltaTime * movementVector);

        } else {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0, ref currentRotationVelocity, movementSmoothFactor);
            characterController.Move(lastMovementDirection * Time.deltaTime * 0);
        }

        // Invocación de evento segura
        OnUpdateMovement?.Invoke(characterController.velocity.magnitude / maximunSpeed);


    }

    private void ApplyGravity()
    {
        if (IsGrounded())
        {
            // Siempre y cuando esté en el suelo, cancelo la velocidad que llevaba
            verticalMovement.y = 0;
        } else {
            // Movimiento en la vertical va a ir decrementándose a X u/s
            verticalMovement.y += gravityScale * Time.deltaTime;
            // Movimiento personaje Y
            characterController.Move(verticalMovement * Time.deltaTime);
        }
        
    }

    private bool IsGrounded() 
    {
        return Physics.CheckSphere(feet.position, detectionRadius, whatIsGround);
    }

    private void OnDisable()
    {
        playerInput.actions["Move"].performed -= UpdateMovement;  
        playerInput.actions["Move"].canceled -= UpdateMovement;  
        playerInput.actions["Jump"].started -= Jump;    
    }

    // Se ejecuta automáticamente para pintar figuras
    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(feet.position, detectionRadius);

    }
}
