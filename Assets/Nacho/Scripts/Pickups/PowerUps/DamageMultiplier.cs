using UnityEngine;

public class DamageMultiplier : PowerUp
{
    [SerializeField] private float damageMultiplierValue = 2f;
    private MovimientoJugador playerMovement;

    protected override void ApplyEffect(GameObject player)
    {
        if (player == null) return;

        playerMovement = player.GetComponent<MovimientoJugador>();
        if (playerMovement != null)
        {
            playerMovement.SetDamageMultiplier(damageMultiplierValue);
        }
    }

    protected override void RemoveEffect(GameObject player)
    {
        if (player == null || playerMovement == null) return;

        playerMovement.SetDamageMultiplier(1f);
    }
}