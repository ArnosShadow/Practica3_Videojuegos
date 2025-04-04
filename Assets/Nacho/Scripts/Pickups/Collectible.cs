using System.Diagnostics;
using UnityEngine;

public class Collectible : Pickup
{
    [SerializeField] private int scoreValue = 100;

    protected override void Collect(GameObject player)
    {
        base.Collect(player);
        
        var scoreManager = FindAnyObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreValue);
        }
        
        Destroy(gameObject);
    }

    protected override void OnPickupCollected()
    {
        base.OnPickupCollected();
        // Debug.Log($"{collectibleName} was collected!");
        // Añadir efecto de sonido o animación aquí
    }
}
