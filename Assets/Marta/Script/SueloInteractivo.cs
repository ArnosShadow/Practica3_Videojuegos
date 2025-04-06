using UnityEngine;

public class SueloInteractivo : MonoBehaviour
{
    
    [SerializeField] private Color colorIluminado;
    [SerializeField] private float intensity ;
    
    //resucción de la velocidad en porcentaje de 0 a 1
    [SerializeField] private float reduccionVelocidad;
    [SerializeField] private float reduccionVida;


    private Renderer rend;
    private Color colorOriginal;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
        colorOriginal= rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rend.material.SetColor("_EmissionColor", colorIluminado* intensity);
            Rigidbody rbPlayer= collision.gameObject.GetComponent<Rigidbody>();
            // MovimientoJugador jugador = collision.gameObject.GetComponent<MovimientoJugador>();

            // if(rbPlayer!= null){
            //     // rbPlayer.mass=10000;
                

            // }

            // if (jugador != null)
            // {
            //     // Debug.Log("Jugador detectado, bajando velocidad.");
            //     // jugador.SetSpeedMultiplier(0);
            // }
            // PruebaPlayerMarta player=collision.gameObject.GetComponent<PruebaPlayerMarta>();
            // player.MovementSpeed=  player.MovementSpeed*reduccionVelocidad;
            // player.RotationSpeed=  player.RotationSpeed*reduccionVelocidad;
            // player.SprintSpeed=  player.SprintSpeed*reduccionVelocidad;

        }
    }

    void OnCollisionExit(Collision collision){
        if (collision.gameObject.CompareTag("Player")) {
            rend.material.SetColor("_EmissionColor", colorOriginal);
            Rigidbody rbPlayer= collision.gameObject.GetComponent<Rigidbody>();
            // PruebaPlayerMarta player=collision.gameObject.GetComponent<PruebaPlayerMarta>();
            // player.MovementSpeed=  player.MovementSpeed/reduccionVelocidad;
            // player.RotationSpeed=  player.RotationSpeed/reduccionVelocidad;
            // player.SprintSpeed=  player.SprintSpeed/reduccionVelocidad;
        }
    }
}
