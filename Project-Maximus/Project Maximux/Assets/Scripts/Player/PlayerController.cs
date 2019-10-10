using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Heath))]
public class PlayerController : MonoBehaviour
{

    public GameObject damageText;

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

    private GameController controller;


    public GameObject deadText;
    public GameObject deadPanel;

    public bool secondAttack = false;

    private int attackCount;
     

    


    // Start is called before the first frame update
    void Start()
    {

        currentHeath = GetComponent<Heath>();
        currentArmor = GetComponent<Armor>();

        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController> ();

        playerBody = GetComponent<Rigidbody>();
        cameraPos = Camera.main.gameObject.transform;

        currentWeapon = weaponObject.GetComponent<Weapon>();

        
        //animator = GetComponent<Animator>();

        inAttackAnimation = false;
        attackCount = 0;

    }

    void updateDeadUI()
    {

       
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

        if (Input.GetKeyDown(KeyCode.Space) && !inAttackAnimation)
        {
            inAttackAnimation = true;

            if (attackCount == 0)
            {
                animator.SetTrigger("swing");
            }
            else if (attackCount == 1) {
                animator.SetTrigger("swing2");
            }
            
            animator.SetBool("attacking", true);
            animator.SetBool("running", false);
        }
        else if(!inAttackAnimation)
        {
            animator.ResetTrigger("swing");
            animator.ResetTrigger("swing2");
            animator.SetBool("attacking", false);
        }else if (inAttackAnimation)
        {
            playerBody.velocity = Vector3.zero;
        }

        updateUI();

    }
    private void FixedUpdate()
    {
        if (!inAttackAnimation)
        {
            movePlayer();
        }
            
        
        
    }

    private void updateUI()
    {
        controller.setHeathValue(currentHeath.getHitPoints(), currentHeath.maxHitPoints);
        controller.setArmorValue(currentArmor.armorValue, currentArmor.maxArmorValue);
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
    

    

    public void attack()
    {
        

        inAttackAnimation = false;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (transform.forward * 2), currentWeapon.reach);
        
        for (int i = 0; i < hitColliders.Length; i++)
        {
            
            Debug.Log(hitColliders[i].gameObject.tag);

            if (hitColliders[i].gameObject.CompareTag("Enemy"))
            {
                GameObject other = hitColliders[i].gameObject;
        
                other.GetComponent<Rigidbody>().AddForce((transform.position - other.transform.position).normalized * 16);

                Enemy enemy = hitColliders[i].gameObject.GetComponent<Enemy>();
                Heath enemyHeath = hitColliders[i].gameObject.GetComponent<Heath>();
                if (attackCount == 0)
                {
                    enemy.takeDamage(Mathf.RoundToInt(Random.Range(currentWeapon.damage - 2, currentWeapon.damage + 2)), false);
                }
                else if (attackCount == 1)
                {
                    enemy.takeDamage(Mathf.RoundToInt(Random.Range(currentWeapon.damage - 1, currentWeapon.damage + 5)), false);
                }
                
                //enemyHeath.reduceHeath(currentWeapon.damage);
                Debug.Log(enemyHeath.getHitPoints());
            }
        }
        if (attackCount == 0)
        {
            attackCount = 1;
        }
        else if (attackCount == 1)
        {

            attackCount = 0;
        }

    }

    public void equipArmor(Armor toEquip)
    {
        currentArmor = toEquip;
    }

    public void takeDamage(float damage)
    {
        GameObject dText = Instantiate(damageText, transform.position + new Vector3(Random.Range(-1, 1), 0.0f, Random.Range(-1, 1)), transform.rotation) as GameObject;
        dText.GetComponent<FloatingText>().currentColor = (new Color(255, 0, 0));
        dText.GetComponent<FloatingText>().text = "-" + damage.ToString();
        if (!currentArmor.isDestroyed())
        {
            currentArmor.takeDamage(damage);
        }
        else {
            currentHeath.reduceHeath(damage);
        }

        if (currentHeath.isDead())
        {
            Destroy(gameObject);
            controller.GetComponent<DeathMenuShow>().setDead(true);
        }

        
    }

    public void resetAnimation()
    {
        animator.SetBool("attacking", false);
        inAttackAnimation = false;

        
    }
}
