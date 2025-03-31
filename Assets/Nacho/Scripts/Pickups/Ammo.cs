using UnityEngine;

public class Ammo : Pickup
{
    protected override void Collect(GameObject player)
    {
        base.Collect(player);
        // Logica para añadir municion al arma equipada.
        Destroy(gameObject);
    }
}
