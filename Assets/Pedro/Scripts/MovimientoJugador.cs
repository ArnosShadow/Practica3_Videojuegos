using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpForce;
    private Animator anim;
    private float x, y;
    private Rigidbody rb;
    public bool canJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canJump = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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
            anim.SetBool("TocarSuelo", true);

        } else {
            Falling();
        }

    }

    private void Falling()
    {
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
