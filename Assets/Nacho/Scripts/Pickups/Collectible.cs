using UnityEngine;

public class Collectible : Pickup
{
    protected override void Collect(GameObject player)
    {
        base.Collect(player);
        // Logica para sumar una puntuacion o coleccionables.
        Destroy(gameObject);
    }

}
