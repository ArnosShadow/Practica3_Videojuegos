using UnityEngine;

/// <summary>
/// Clase que detecta si los pies del personaje están en contacto con el suelo,
/// permitiendo o impidiendo el salto.
/// </summary>
public class PiesPersonaje : MonoBehaviour
{
    // Referencia al script MovimientoJugador para controlar el salto
    [SerializeField] private MovimientoJugador movimientoJugador;

    /// <summary>
    /// Se ejecuta mientras el personaje está en contacto con un objeto.
    /// Permite el salto al jugador.
    /// </summary>
    /// <param name=""other">Collider del objeto en contacto</param>
    private void OnTriggerStay(Collider other)
    {
        movimientoJugador.canJump = true;    
    }

    /// <summary>
    /// Se ejecuta cuando el personaje deja de estar en contacto con un objeto.
    /// Impide el salto al jugador.
    /// </summary>
    /// <param name=""other">Collider del objeto que deja de estar en contacto</param>
    private void OnTriggerExit(Collider other)
    {
        movimientoJugador.canJump = false;
    }
}