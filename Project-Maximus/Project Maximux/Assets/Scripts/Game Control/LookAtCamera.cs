using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Transform cameraPos = Camera.main.transform;
        Vector3 lookRot = transform.position - cameraPos.position;

        Quaternion newRotation = Quaternion.LookRotation(lookRot);
        transform.rotation = newRotation;
    }
}
