using UnityEngine;

public abstract class PowerUp : Pickup
{
    [SerializeField] protected float duration = 10f;
    [SerializeField] protected float multiplier = 2f;
    
    protected bool isActive = false;
    protected float timeRemaining;

    protected override void Collect(GameObject player)
    {
        base.Collect(player);
        ApplyEffect(player);
        timeRemaining = duration;
        isActive = true;
        StartCoroutine(PowerUpRoutine(player));
    }

    protected abstract void ApplyEffect(GameObject player);
    protected abstract void RemoveEffect(GameObject player);

    private System.Collections.IEnumerator PowerUpRoutine(GameObject player)
    {
        yield return new WaitForSeconds(duration);
        RemoveEffect(player);
        isActive = false;
        Destroy(gameObject);
    }
}