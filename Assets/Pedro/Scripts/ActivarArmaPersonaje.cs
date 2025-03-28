using UnityEngine;

/// <summary>
/// Clase encargada de activar un arma para el personaje cuando colisiona con un objeto específico.
/// </summary>
public class ActivarArmaPersonaje : MonoBehaviour
{
    // Referencia al script CogerArma, que maneja el sistema de armas del jugador
    public CogerArma cogerArma;
    
    // Número de arma que se activará al recoger el objeto
    [SerializeField] private int numArma;

    /// <summary>
    /// Se ejecuta al inicio del juego, asignando la referencia al componente CogerArma del jugador.
    /// </summary>
    void Start()
    {
        // Busca el objeto con la etiqueta "Player" en la escena
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        // Si el jugador existe en la escena, obtenemos su componente CogerArma
        if (player != null)
        {
            cogerArma = player.GetComponent<CogerArma>();
        }
    }

    /// <summary>
    /// Método llamado cuando otro objeto entra en el trigger del objeto actual.
    /// </summary>
    /// <param name=""other">Collider del objeto que entra en la zona de colisión</param>
    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto con el que colisionamos es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            // Activamos el arma correspondiente en el script CogerArma
            cogerArma.ActivarArma(numArma);
            
            // Destruimos este objeto después de activar el arma
            Destroy(gameObject);
        }
    }
}
