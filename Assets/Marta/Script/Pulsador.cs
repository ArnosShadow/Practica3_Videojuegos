using UnityEngine;

public class Pulsador : MonoBehaviour
{

    [SerializeField] private Tramp trampa;
    
    void ActiveTrap(){
        if(trampa!= null){
            trampa.Activar();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveTrap();
        }
    }
}
