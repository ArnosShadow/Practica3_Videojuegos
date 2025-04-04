using UnityEngine;

public class HealthPack : Pickup
{
    [SerializeField] private float healAmount = 25f;

    protected override void Collect(GameObject player)
    {
        base.Collect(player);

        var health = player.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(healAmount);
        }
        
        Destroy(gameObject);
    }

    protected override void OnPickupCollected()
    {
        base.OnPickupCollected();
        // Debug.Log($"Health Pack was collected!");
        // Añadir particulas de salud, efecto de sonido, etc.
    }
}
