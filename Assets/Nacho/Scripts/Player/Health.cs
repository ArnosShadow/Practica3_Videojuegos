using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    
    private bool isInvincible = false;
    
    public UnityEvent<float> onHealthChanged;
    public UnityEvent onDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;
        
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        onHealthChanged?.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        onHealthChanged?.Invoke(currentHealth / maxHealth);
    }

    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
    }

    private void Die()
    {
        onDeath?.Invoke();
        // Aqui se moriria el personaje y resetear el nivel
    }
}