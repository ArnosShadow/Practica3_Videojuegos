using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityExtra;
    [SerializeField] private CapsuleCollider colliderEnPie;
    [SerializeField] private CapsuleCollider colliderAgachado;
    [SerializeField] private GameObject head;
    [SerializeField] private CabezaPersonaje cabezaPersonaje;
    private bool estoyAgachado;
    private Animator anim;
    private float x, y;
    private Rigidbody rb;
    public bool canJump;
    private float initialSpeed;
    private float crouchSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canJump = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        initialSpeed = movementSpeed;
        crouchSpeed = movementSpeed * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Declaración de variables respecto a sus ejes
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Configuramos nuestro animator
        anim.SetFloat("VelocidadX", x);
        anim.SetFloat("VelocidadY", y);

        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("EstoySaltando", true);
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                anim.SetBool("Agachado", true);
                movementSpeed = crouchSpeed;

                // Cambio de collider
                colliderAgachado.enabled = true;
                colliderEnPie.enabled = false;

                head.SetActive(true);
                estoyAgachado = true;
                
            } else {

                if (cabezaPersonaje.collisionCount <= 0)
                {
                    anim.SetBool("Agachado", false);
                    movementSpeed = initialSpeed;

                    // Cambio de collider
                    head.SetActive(false);
                    colliderAgachado.enabled = false;
                    colliderEnPie.enabled = true;
                    estoyAgachado = false;

                }



                
            }

            anim.SetBool("TocarSuelo", true);

        } else {
            Falling();
        }

    }

    private void Falling()
    {
        // Hacemos que nuestro personaje caiga más rápido
        rb.AddForce(gravityExtra * Physics.gravity);
        anim.SetBool("TocarSuelo", false);
        anim.SetBool("EstoySaltando", false);

    }

    private void FixedUpdate()
    {     
        // Funciones para el movimeinto del jugador
        transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0);
        transform.Translate(0, 0, y * movementSpeed * Time.deltaTime);
    }
}
