using UnityEngine;

public class InvincibilityPowerUp : PowerUp
{
    private Health playerHealth;

    protected override void ApplyEffect(GameObject player)
    {
        playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.SetInvincible(true);
        }
    }

    protected override void RemoveEffect(GameObject player)
    {
        if (playerHealth != null)
        {
            playerHealth.SetInvincible(false);
        }
    }
}