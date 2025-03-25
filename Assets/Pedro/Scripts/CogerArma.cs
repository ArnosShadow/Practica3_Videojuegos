using UnityEngine;

public class CogerArma : MonoBehaviour
{
    public GameObject[] armas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarArma(int num)
    {
        for (int i = 0; i < armas.Length; i++)
        {
            armas[i].SetActive(false);
        }

        armas[num].SetActive(true);

    }
}
