using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject heathBar, armorBar;


    public GameObject getHeathBar()
    {
        return heathBar;
    }

    public GameObject getArmorBar()
    {
        return armorBar;
    }

    public void setHeathValue(float value, float max)
    {
        heathBar.GetComponent<BarController>().setValue(value, max);
       
    }

    public void setArmorValue(float value, float max)
    {
        armorBar.GetComponent<BarController>().setValue(value, max);
    }
}
