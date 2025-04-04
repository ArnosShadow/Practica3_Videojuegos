using UnityEngine;

public class Pickup : MonoBehaviour
{
    #region
    [SerializeField] protected string collectibleName;
    [SerializeField] protected string description;
    [SerializeField] protected float rotationSpeed = 50.0f;
    [SerializeField] protected float bobSpeed = 2.0f;
    [SerializeField] protected float bobHeight = 0.5f;
    
    private bool isCollected = false;
    private Vector3 startPosition;
    #endregion
    
    protected virtual void Start()
    {
        startPosition = transform.position;
    }

    protected virtual void Update()
    {
        if(!isCollected)
        {
            // Efecto de rotacion
            transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
            
            // Efecto de bob
            float newY = startPosition.y + (Mathf.Sin(Time.time * bobSpeed) * bobHeight);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            Collect(other.gameObject);
        }
    }

    protected virtual void Collect(GameObject player)
    {
        isCollected = true;
        OnPickupCollected();
    }

    protected virtual void OnPickupCollected()
    {
        Debug.Log($"{collectibleName} was collected!");
    }
}
