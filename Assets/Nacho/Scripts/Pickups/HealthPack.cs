using UnityEngine;

public class HealthPack : Pickup
{
    protected override void Collect(GameObject player)
    {
        base.Collect(player);
        // Logica para curar al Jugador
        Destroy(gameObject);
    }
}
