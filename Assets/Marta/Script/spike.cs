using UnityEngine;

public class spike : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb= collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("Muerto");
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }
}
