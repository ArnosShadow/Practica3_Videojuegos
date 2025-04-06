using UnityEngine;

public class FallingTile : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (col != null)
        {
            col.isTrigger = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            if (col != null)
            {
                col.isTrigger = true;
            }
        }
    }
}
