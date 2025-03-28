using UnityEngine;

/// <summary>
/// Clase que controla la detección de colisiones en la cabeza del personaje.
/// Lleva un conteo de los objetos con los que entra en contacto.
/// </summary>
public class CabezaPersonaje : MonoBehaviour
{
    // Contador de colisiones activas
    public int collisionCount = 0;

    /// <summary>
    /// Se ejecuta cuando otro objeto entra en el trigger de la cabeza del personaje.
    /// </summary>
    /// <param name=""other">Collider del objeto que entra en contacto</param>
    void OnTriggerEnter(Collider other)
    {
        collisionCount++; // Incrementa el contador al detectar una colisión
    }

    /// <summary>
    /// Se ejecuta cuando otro objeto sale del trigger de la cabeza del personaje.
    /// </summary>
    /// <param name=""other">Collider del objeto que deja de estar en contacto</param>
    void OnTriggerExit(Collider other)
    {
        collisionCount--; // Decrementa el contador cuando el objeto deja de estar en contacto
    }
}
