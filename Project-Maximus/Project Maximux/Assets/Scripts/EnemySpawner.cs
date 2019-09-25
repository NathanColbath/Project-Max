using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

  

    public GameObject[] enemysToSpawn;

    public int minAmountToSpawn;
    public int maxAmountToSpawn;

    public float radiusToSpawn;

    //Always spawn the first enemy, and then spawn a random amount of the next enemys

    private void Start()
    {
        spawnEnemys();
    }

    public void spawnEnemys()
    {
        


        int amountToSpawn = Random.Range(minAmountToSpawn, maxAmountToSpawn);

        Instantiate(enemysToSpawn[0], randomPointInCircle(radiusToSpawn), Quaternion.identity); //Boss Enemy

        if(enemysToSpawn.Length == 1)
        {
            return;
        }

        for (int i = 0; i < amountToSpawn; i++)
        {

            Instantiate(enemysToSpawn[Random.Range(1, enemysToSpawn.Length - 1)], randomPointInCircle(radiusToSpawn), Quaternion.identity);

        }


    }

    private Vector3 randomPointInCircle(float radius)
    {

        Vector2 randomDir = Quaternion.Euler(0.0f, 0.0f, Random.Range(0,360)) * Vector2.right * radius + transform.position;

        Vector3 toReturn = new Vector3(randomDir.x, 0.0f, randomDir.y);

        return toReturn;
    }
}
