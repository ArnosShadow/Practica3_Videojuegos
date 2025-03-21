using UnityEngine;

public class PiesPersonaje : MonoBehaviour
{
    [SerializeField] private MovimientoJugador movimientoJugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        movimientoJugador.canJump = true;    
    }

    void OnTriggerExit(Collider other)
    {
        movimientoJugador.canJump = false;
    }
}
