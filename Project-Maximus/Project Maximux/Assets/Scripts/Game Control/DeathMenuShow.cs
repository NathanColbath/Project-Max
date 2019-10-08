using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenuShow : MonoBehaviour
{

    public GameObject deathText;
    public GameObject deathPanel;

    private Color deathTextColor;
    private Color deathPanelColor;

    private bool dead;
    private float alpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        deathTextColor = deathText.GetComponent<Text>().color;
        deathPanelColor = deathPanel.GetComponent<Image>().color;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            deathText.SetActive(true);
            deathPanel.SetActive(true);

            deathText.GetComponent<Text>().color = new Color(deathTextColor.r, deathTextColor.b, deathTextColor.g, alpha);
            deathPanel.GetComponent<Image>().color = new Color(deathPanelColor.r, deathPanelColor.b, deathPanelColor.g, alpha);

            alpha += 0.005f;
        }
    }

    public void setDead(bool death)
    {
        dead = death;
    }
}
