using UnityEngine;

public class CabezaPersonaje : MonoBehaviour
{
    public int collisionCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        collisionCount++;
    }

    void OnTriggerExit(Collider other)
    {
        collisionCount--;
    }
}
