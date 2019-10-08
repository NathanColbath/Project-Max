using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    Transform barValue;
    void Start()
    {
        barValue = transform.Find("Value");
        //barValue.localScale = new Vector2(.4f, barValue.localScale.y);
    }

    public void setValue(float value, float maxValue)
    {
        //float size = (value * 100) / maxValue;
        float size = value / maxValue;
        if (size <= 0)
        {
            size = 0;
        }

        barValue.localScale = new Vector2(size, barValue.localScale.y); ;

        
    }
}
