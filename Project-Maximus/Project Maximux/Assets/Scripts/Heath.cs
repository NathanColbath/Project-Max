using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heath : MonoBehaviour
{
    public float hitPoints;
    public float maxHitPoints;


    private void Start()
    {
        if(maxHitPoints == 0)
        {
            Debug.LogError("HEATH COMPONENT VARIABLES NOT SET PROPERLY");
        }
    }

    public bool isDead()
    {
        if(hitPoints <= 0)
        {
            return true;
        }
        return false;
    }

    public void addHeath(float toAdd)
    {
        hitPoints += toAdd;
        if(hitPoints >= maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
    }

    public void reduceHeath(float toTake)
    {
        hitPoints -= toTake;
    }

    public void setMaxHitPoints(float newMax)
    {
        maxHitPoints = newMax;
    }

    public void kill()
    {
        hitPoints = 0;
    }

    public float getHitPoints()
    {
        return hitPoints;
    }
}
