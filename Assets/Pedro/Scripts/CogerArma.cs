using UnityEngine;

public class CogerArma : MonoBehaviour
{
    //[SerializeField] private BoxCollider[] armasBoxColl;
    //[SerializeField] private BoxCollider punioBoxColl;
    public GameObject[] armas;
    [SerializeField] private MovimientoJugador movimientoJugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //DesactivarColliderArmas();
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
        movimientoJugador.conArma = true;

    }

    // private void ActivarColliderArmas()
    // {
    //     for (int i = 0; i < armasBoxColl.Length; i++)
    //     {
    //         if (movimientoJugador.conArma)
    //         {
    //             armasBoxColl[i].enabled = true;
    //         } else {
    //             punioBoxColl.enabled = true;
    //         }
    //     }
    // }

    // private void DesactivarColliderArmas()
    // {
    //     for (int i = 0; i < armasBoxColl.Length; i++)
    //     {
    //         armasBoxColl[i].enabled = false;
    //     }
    //     punioBoxColl.enabled = false;

    // }
}
