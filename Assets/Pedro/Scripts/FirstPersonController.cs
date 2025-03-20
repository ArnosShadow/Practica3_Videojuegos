using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Obligar que el gameObject tenga un CharacterController
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravityScale;
    [SerializeField] private float detectionRadius;

    [Header("Ground Detection")]
    [SerializeField] private Transform feet;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;

    private Camera cam;
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Vector2 movementInput;
    private Vector3 verticalMovement; // Sirve para gravedad y para saltar

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
        transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
        // Si existe input...
        if (movementInput.sqrMagnitude > 0)
        {
            // Se calcula el ángulo en base a los inputs + la rotación de la cámara
            float angleToRotate = Mathf.Atan2(movementInput.x, movementInput.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            // Se rota el vector 0, 0, 1 al ángulo calculado en el paso anterior
            Vector3 movementVector = Quaternion.Euler(0, angleToRotate, 0) * Vector3.forward;

            // Moviemiento personaje en X Z
            characterController.Move(movementSpeed * Time.deltaTime * movementVector);
        }


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
