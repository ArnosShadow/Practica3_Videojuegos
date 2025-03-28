using UnityEngine;

/// <summary>
/// Clase encargada de gestionar la activación de armas para el jugador.
/// </summary>
public class CogerArma : MonoBehaviour
{
    // Array que contiene las armas disponibles
    public GameObject[] armas;
    
    // Referencia al script MovimientoJugador para actualizar el estado del jugador
    [SerializeField] private MovimientoJugador movimientoJugador;

    /// <summary>
    /// Activa un arma específica y desactiva las demás.
    /// </summary>
    /// <param name=""num">Índice del arma a activar</param>
    public void ActivarArma(int num)
    {
        // Desactiva todas las armas
        for (int i = 0; i < armas.Length; i++)
        {
            armas[i].SetActive(false);
        }

        // Activa el arma seleccionada
        armas[num].SetActive(true);
        
        // Indica que el jugador ahora tiene un arma equipada
        movimientoJugador.conArma = true;
    }
}
