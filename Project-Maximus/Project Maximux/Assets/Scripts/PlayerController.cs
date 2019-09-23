using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 10;
    private Rigidbody playerBody;

    private float moveX, moveZ;
    public float maxSpeed = 5;

    private Transform cameraPos;
    private Vector3 forwardVec;
    private Vector3 rightVec;


    // Start is called before the first frame update
    void Start()
    {
      

        playerBody = GetComponent<Rigidbody>();
        cameraPos = Camera.main.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        forwardVec = transform.position - cameraPos.position;
        forwardVec = forwardVec.normalized;

        

    }
    private void FixedUpdate()
    {
        movePlayer();
    }


    private void movePlayer()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveVec = new Vector3(0,0,0);


        /*if (Input.GetKey(KeyCode.W))
        {
            //playerBody.AddForce(forwardVec * moveSpeed);
            moveVec += new Vector3(forwardVec.x, 0.0f, forwardVec.z);
            //playerBody.AddForce(moveVec * moveSpeed);

            

        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVec += cameraPos.transform.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVec  += -cameraPos.transform.right;
        }*/

        moveVec = new Vector3(moveX, 0.0f, moveZ);


        playerBody.velocity = moveVec * moveSpeed;

    }

    void clampPlayer()
    {
        if(playerBody.velocity.magnitude > maxSpeed)
        {
            //TODO
        }
    }
}
