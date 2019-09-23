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

    public GameObject weaponObject;

    private Weapon currentWeapon;
    public GameObject weaponHolder;


    // Start is called before the first frame update
    void Start()
    {
      

        playerBody = GetComponent<Rigidbody>();
        cameraPos = Camera.main.gameObject.transform;

        currentWeapon = weaponObject.GetComponent<Weapon>();


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

        moveVec = new Vector3(moveX, 0.0f, moveZ);


        playerBody.velocity = moveVec * moveSpeed;

    }

    public void equipWeapon(Weapon toEquip)
    {
        currentWeapon = toEquip;
    }

    public void attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.forward * 5, currentWeapon.reach);

        for(int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = hitColliders[i].gameObject.GetComponent<Enemy>();
                Heath enemyHeath = hitColliders[i].gameObject.GetComponent<Heath>();
            }
        }
    }
}
