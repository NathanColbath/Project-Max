using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Heath))]
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

    public Armor currentArmor;
    public Heath currentHeath;

    private float lerpSpeed = .25f;

    public Animator animator;

    private bool inAttackAnimation;


    // Start is called before the first frame update
    void Start()
    {


        playerBody = GetComponent<Rigidbody>();
        cameraPos = Camera.main.gameObject.transform;

        currentWeapon = weaponObject.GetComponent<Weapon>();

        
        //animator = GetComponent<Animator>();

        inAttackAnimation = false;

    }

    // Update is called once per frame
    void Update()
    {
        forwardVec = transform.position - cameraPos.position;
        forwardVec = forwardVec.normalized;


        //animator.SetBool("running", true);

        if (checkIfGettingInput() && !inAttackAnimation)
        {
            animator.SetBool("running", true);
        } else if (!checkIfGettingInput() || inAttackAnimation)
        {
            animator.SetBool("running", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inAttackAnimation = true;
            animator.SetTrigger("swing");
            animator.SetBool("attacking", true);
            animator.SetBool("running", false);
        }
        else
        {
            animator.ResetTrigger("swing");
            animator.SetBool("attacking", false);
        }

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

        //moveVec = new Vector3(moveX, 0.0f, moveZ);

        if (Input.GetKey(KeyCode.W))
        {
            //playerBody.AddForce(forwardVec * moveSpeed);
            moveVec += new Vector3(forwardVec.x, 0.0f, forwardVec.z * Vector3.forward.z).normalized;
            //playerBody.AddForce(moveVec * moveSpeed);



        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVec += cameraPos.transform.right.normalized;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVec += -cameraPos.transform.right.normalized;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVec += new Vector3(-forwardVec.x, 0.0f, -forwardVec.z * Vector3.forward.z).normalized;
        }

        

        if (checkIfGettingInput())
        {
            
            Quaternion newRotation = Quaternion.LookRotation(moveVec);
            playerBody.rotation = Quaternion.Lerp(playerBody.rotation,newRotation, lerpSpeed);
            
        }





        if (!inAttackAnimation)
        {
            playerBody.velocity = moveVec * moveSpeed;
        }else
        {
            playerBody.velocity = Vector3.zero;
        }
        

        

    }

    private void attackInput()
    {
        
    }

    private bool checkIfGettingInput()
    {
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            
            return false;
            
        }
        
        return true;
    }



    public void equipWeapon(Weapon toEquip)
    {
        currentWeapon = toEquip;
    }
    

    private void getAttackInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = new Ray(pos, Vector3.down);
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                playerBody.rotation = Quaternion.LookRotation( hit.point);


            }

        }
    }

    public void attack()
    {

       

        inAttackAnimation = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (transform.forward * 2), currentWeapon.reach);

        for(int i = 0; i < hitColliders.Length; i++)
        {

            Debug.Log(hitColliders[i].gameObject.tag);

            if (hitColliders[i].gameObject.CompareTag("Enemy"))
            {
                
                Enemy enemy = hitColliders[i].gameObject.GetComponent<Enemy>();
                Heath enemyHeath = hitColliders[i].gameObject.GetComponent<Heath>();

                enemyHeath.reduceHeath(currentWeapon.damage);
                Debug.Log(enemyHeath.getHitPoints());
            }
        }
    }

    public void equipArmor(Armor toEquip)
    {
        currentArmor = toEquip;
    }

    public void takeDamage(float damage)
    {
        if (!currentArmor.isDestroyed())
        {
            currentArmor.takeDamage(damage);
        }
        else {
            currentHeath.reduceHeath(damage);
        }
    }
}
