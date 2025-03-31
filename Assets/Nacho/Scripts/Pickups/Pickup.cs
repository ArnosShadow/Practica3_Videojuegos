using UnityEngine;

public class Pickup : MonoBehaviour
{
    #region
    [SerializeField] protected string collectibleName;
    [SerializeField] protected string description;
    [SerializeField] protected float rotationSpeed = 50.0f;
    private bool isCollected = false;
    #endregion
    
    protected virtual void Update()
    {
        if(!isCollected){
            transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            Collect(other.gameObject);
        }
    }

    protected virtual void Collect(GameObject other)
    {
        isCollected = true;
    }
}
