using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    public string text;
    public float moveSpeed = 2;

    private TextMesh textMesh;

    private Color textColor;
    public Color currentColor;

    private float alpha;
    void Start()
    {
        alpha = 1;
        //textColor = new Color(255, 255, 255, 255);
        textMesh = GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.anchor = TextAnchor.MiddleCenter;
        //currentColor = new Color(255, 255, 255);


    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f);
    }

    private void checkToDelete()
    {
        if(alpha <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        textColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        Transform cameraPos = Camera.main.transform;

        alpha -= .01f;

        Vector3 lookRot = transform.position - cameraPos.position;

        Quaternion newRotation = Quaternion.LookRotation(lookRot);
        transform.rotation = newRotation;


        textMesh.color = textColor;
        checkToDelete();
    }

}
