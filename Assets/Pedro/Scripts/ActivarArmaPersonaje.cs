using UnityEngine;

public class ActivarArmaPersonaje : MonoBehaviour
{
    public CogerArma cogerArma;
    [SerializeField] int numArma;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            cogerArma = player.GetComponent<CogerArma>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cogerArma.ActivarArma(numArma);
            Destroy(gameObject);
        }
    }
}
