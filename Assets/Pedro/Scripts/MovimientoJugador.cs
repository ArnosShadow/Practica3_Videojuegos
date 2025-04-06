using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla el movimiento del jugador, incluyendo caminar, correr, saltar y agacharse.
/// También maneja la animación y la detección de ataques.
/// </summary>
public class MovimientoJugador : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchSpeedFactor = 0.5f;
    
    [Header("Configuración de Salto y Gravedad")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityExtra;
    
    [Header("Colliders y Objetos")]
    [SerializeField] private CapsuleCollider colliderEnPie;
    [SerializeField] private CapsuleCollider colliderAgachado;
    [SerializeField] private GameObject head;
    [SerializeField] private CabezaPersonaje cabezaPersonaje;
    [SerializeField] private BoxCollider rightHand;
    [SerializeField] private BoxCollider[] armasCollider;
    
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
        ManejarSaltoYAgachado();
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
    private void ManejarSaltoYAgachado()
    {
        if (canJump)
        {
            if (!atacando)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("EstoySaltando", true);
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    AgacharJugador();
                }
                else if (cabezaPersonaje.collisionCount <= 0)
                {
                    LevantarJugador();
                }
            }
            anim.SetBool("TocarSuelo", true);
        }
        else
        {
            Falling();
        }
    }

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
    /// Maneja la lógica de agacharse.
    /// </summary>
    private void AgacharJugador()
    {
        anim.SetBool("Agachado", true);
        colliderAgachado.enabled = true;
        colliderEnPie.enabled = false;
        head.SetActive(true);
        estoyAgachado = true;
    }

    /// <summary>
    /// Maneja la lógica de levantarse después de agacharse.
    /// </summary>
    private void LevantarJugador()
    {
        anim.SetBool("Agachado", false);
        head.SetActive(false);
        colliderAgachado.enabled = false;
        colliderEnPie.enabled = true;
        estoyAgachado = false;
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
    private void AvanzoAtaque()
    {
        avanzoAtaque = true;

        for (int i = 0; i < armasCollider.Length; i++)
        {
            if (conArma)
            {
                armasCollider[i].enabled = true;
            } else {
                rightHand.enabled = true;
            }
        }
    }

    /// <summary>
    /// Detiene el movimiento hacia adelante en un ataque.
    /// </summary>
    private void DejoDeAvanzar()
    {
        avanzoAtaque = false;
        rightHand.enabled = false;

        for (int i = 0; i < armasCollider.Length; i++)
        {
            armasCollider[i].enabled = false;
        }
        rightHand.enabled = false;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
        movementSpeed = initialSpeed * speedMultiplier;
    }
    public void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Finish"))
        {

            SceneManager.LoadScene("CargarEscena");
        }
    }
}
