using UnityEngine;

public class SpeedBoost : PowerUp
{
    private MovimientoJugador playerMovement;

    protected override void ApplyEffect(GameObject player)
    {
        playerMovement = player.GetComponent<MovimientoJugador>();
        if (playerMovement != null)
        {
            playerMovement.SetSpeedMultiplier(multiplier);
        }
    }

    protected override void RemoveEffect(GameObject player)
    {
        if (playerMovement != null)
        {
            playerMovement.SetSpeedMultiplier(1f);
        }
    }
}