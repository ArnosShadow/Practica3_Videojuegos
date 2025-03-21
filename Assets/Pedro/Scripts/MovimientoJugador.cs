using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    private Animator anim;
    private float x, y;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Declaración de variables respecto a sus ejes
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Funciones para el movimeinto del jugador
        transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0);
        transform.Translate(0, 0, y * movementSpeed * Time.deltaTime);

        // Configuramos nuestro animator
        anim.SetFloat("VelocidadX", x);
        anim.SetFloat("VelocidadY", y);

        
    }
}
