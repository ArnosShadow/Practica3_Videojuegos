using UnityEngine;

public class PruebaPlayerMarta : MonoBehaviour
{
   [Header("Configuración de Movimiento")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchSpeedFactor = 0.5f;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float SprintSpeed { get => sprintSpeed; set => sprintSpeed = value; }
    
    [Header("Configuración de Salto y Gravedad")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityExtra;
    
    
    [Header("Configuración de Ataque")]
    private bool atacando;
    private bool avanzoAtaque;
    public bool conArma;
    private float impulsoGolpe = 2f;
    private float damageMultiplier = 1f;  

    // Variables internas de estado
    private bool estoyAgachado;
    private Animator anim;
    private float x, y;
    private Rigidbody rb;
    public bool canJump;
    private float initialSpeed;
    private float speedMultiplier = 1f;
    
    // Método Start: Se ejecuta una vez al inicio
    void Start()
    {
        canJump = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        initialSpeed = movementSpeed;
        crouchSpeedFactor *= movementSpeed; // Define la velocidad de agachado
    }

    // Método Update: Se ejecuta una vez por frame
    void Update()
    {
        ManejarEntradaMovimiento();
        ManejarSprint();
        ManejarAtaque();
        // ManejarSaltoYAgachado();
    }

    // Método FixedUpdate: Se usa para física y movimiento
    private void FixedUpdate()
    {    
        if (!atacando)
        {
            transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0);
            transform.Translate(0, 0, y * movementSpeed * Time.deltaTime);
        } 

        if (avanzoAtaque)
        {
            rb.linearVelocity = transform.forward * (impulsoGolpe * damageMultiplier);
        }
    }  

    /// <summary>
    /// Maneja la entrada del usuario para el movimiento.
    /// </summary>
    private void ManejarEntradaMovimiento()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        anim.SetFloat("VelocidadX", x);
        anim.SetFloat("VelocidadY", y);
    }

    /// <summary>
    /// Controla la lógica del sprint del jugador.
    /// </summary>
    private void ManejarSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !estoyAgachado && canJump && !atacando)
        {
            movementSpeed = sprintSpeed;
            anim.SetBool("Sprint", y > 0);
        }
        else
        {
            anim.SetBool("Sprint", false);
            movementSpeed = estoyAgachado ? crouchSpeedFactor : initialSpeed;
        }
    }

    /// <summary>
    /// Maneja la lógica del ataque.
    /// </summary>
    private void ManejarAtaque()
    {
        if (Input.GetKeyDown(KeyCode.E) && canJump && !atacando)
        {
            if (conArma)
            {
                anim.SetTrigger("GolpeoArma");
                atacando = true;
            } else {
                anim.SetTrigger("Golpeo");
                atacando = true;
            }
        }
    }

    /// <summary>
    /// Maneja el salto y el agachado del jugador.
    /// </summary>


    /// <summary>
    /// Aplica fuerza adicional para acelerar la caída del jugador.
    /// </summary>
    private void Falling()
    {
        rb.AddForce(gravityExtra * Physics.gravity);
        anim.SetBool("TocarSuelo", false);
        anim.SetBool("EstoySaltando", false);
    }

   

    /// <summary>
    /// Finaliza el ataque del jugador.
    /// </summary>
    private void FinAtaque()
    {
        atacando = false;
    }

    /// <summary>
    /// Inicia el movimiento hacia adelante en un ataque.
    /// </summary>
   

    /// <summary>
    /// Detiene el movimiento hacia adelante en un ataque.
    /// </summary>
   

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
        movementSpeed = initialSpeed * speedMultiplier;
    }
    public void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }
}
