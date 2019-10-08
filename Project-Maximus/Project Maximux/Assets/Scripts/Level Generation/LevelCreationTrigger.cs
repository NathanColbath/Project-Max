using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreationTrigger : MonoBehaviour
{
    public Transform levelBase;
    public bool generatedNewLevel;

    public LevelController levelController;

    private void Start()
    {
        generatedNewLevel = false;
        levelController = GameObject.FindGameObjectWithTag("Level Controller").GetComponent<LevelController> ();
    }

    private void OnTriggerEnter(Collider other)
    {

        

        if (!generatedNewLevel)
        {
            if (other.CompareTag("Player"))
            {
                //levelController.createRandomLevel(levelBase.position, levelBase.rotation, levelBase.forward);
                //Debug.LogError("HIT");
               // generatedNewLevel = true;
            }
            
        }
        
    }
}
