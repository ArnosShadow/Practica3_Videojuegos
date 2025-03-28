using UnityEngine;

/// <summary>
/// Clase que representa un enemigo en el juego.
/// Se destruye cuando colisiona con un objeto que tenga la etiqueta "Golpe".
/// </summary>
public class Enemigo : MonoBehaviour
{
    /// <summary>
    /// Se ejecuta cuando otro objeto entra en el trigger del enemigo.
    /// </summary>
    /// <param name=""other">Collider del objeto que entra en contacto</param>
    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto que colisiona tiene la etiqueta "Golpe", el enemigo se destruye
        if (other.gameObject.CompareTag("Golpe"))
        {
            Destroy(gameObject);
        }
    }
}
