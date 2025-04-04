using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    public int anchoCampo = 20;
    public int altoCampo = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}