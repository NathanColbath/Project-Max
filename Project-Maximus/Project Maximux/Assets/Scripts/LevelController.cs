using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject toCreateFrom;

    private Vector3[] randomDirection;


    // Start is called before the first frame update
    void Start()
    {
        randomDirection = new Vector3[4];

        randomDirection[0] = Vector3.forward;
        randomDirection[1] = Vector3.back;
        randomDirection[2] = Vector3.right;
        randomDirection[3] = Vector3.left;
        
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            createRandomLevel(toCreateFrom.transform.position, toCreateFrom.transform.rotation, toCreateFrom.transform.forward);
        }
    }


    public void createRandomLevel(Vector3 position, Quaternion rotation, Vector3 direction)
    {
        

        int levelIndex = Random.Range(0, levels.Length);
        GameObject level = levels[levelIndex];
        Vector3 levelSize = level.GetComponent<Collider>().bounds.size;


        Vector3 offset = new Vector3(direction.x * levelSize.x, direction.y * levelSize.y, direction.z * levelSize.z);

        //TESTING
        Debug.Log(levelSize);

        Instantiate(level, position + offset  , rotation);
    }
}
