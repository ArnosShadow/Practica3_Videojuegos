using UnityEngine;

public class RollingBall : Tramp
{
    [SerializeField] private Transform objetivo;
    [SerializeField] private GameObject pared;
    [SerializeField] private float speed;
    private Rigidbody rb;
    private Vector3 positionObjectivo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb=GetComponent<Rigidbody>();
        // MoveBall();
        // positionObjectivo= objetivo.position;
        rb.isKinematic = true;
        rb.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,positionObjectivo, speed * Time.deltaTime);
        // MoveBall();
        // rb.linearVelocity= Vector3.forward;

    }
    

    void MoveBall(){
        // if(objetivo!=null){
        //     positionObjectivo= objetivo.position;
        //     // transform.position = Vector3.MoveTowards(transform.position, objetivo.position, speed * Time.deltaTime);
        // }
        transform.position = Vector3.MoveTowards(transform.position,positionObjectivo, speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.IsChildOf(pared.transform)){
        // if(collision.gameObject.name=="BloqueParedEntero"){
            // MoveBall();
            positionObjectivo= objetivo.position;
            // transform.position = Vector3.MoveTowards(transform.position, objetivo.position, speed * Time.deltaTime);

        }

        if(collision.gameObject.CompareTag("palyer")){
            GameObject player = collision.gameObject;

            // player.MuerteInstantanea();
        }
        
    }

    public override void Activar(){
        positionObjectivo= objetivo.position;
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
