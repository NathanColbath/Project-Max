using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heath : MonoBehaviour
{
    public float hitPoints;
    public float maxHitPoints;

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

    public void setMaxHitPoints(float newMax)
    {
        maxHitPoints = newMax;
    }

    public void kill()
    {
        hitPoints = 0;
    }
}
