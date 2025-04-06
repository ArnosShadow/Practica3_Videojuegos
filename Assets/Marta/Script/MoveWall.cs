using UnityEngine;

public class MoveWall : Tramp
{
     [SerializeField] float moveSpeed;


    private Transform leftWall, rightWall;
    // private Vector3 leftPos, rightPos;
    private bool isActivate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        isActivate=false;
        leftWall= transform.Find("ParedMovilLeft");
        rightWall= transform.Find("ParedMovilRight");

        // leftPos=leftWall.position;
        // rightPos=rightWall.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(isActivate){
            leftWall.position=Vector3.MoveTowards(leftWall.position, rightWall.position, moveSpeed*Time.deltaTime );
            rightWall.position=Vector3.MoveTowards(rightWall.position, leftWall.position, moveSpeed*Time.deltaTime );
        }
    }

    public override void Activar(){
        isActivate= true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            GameObject player = collision.gameObject;

            // player.MuerteInstantanea();
        }
    }
}
