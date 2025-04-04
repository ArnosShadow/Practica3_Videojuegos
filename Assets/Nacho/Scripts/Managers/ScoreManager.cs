using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } }

    [SerializeField] private int currentScore = 0;
    public UnityEvent<int> onScoreChanged;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        onScoreChanged?.Invoke(currentScore);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        onScoreChanged?.Invoke(currentScore);
    }
}