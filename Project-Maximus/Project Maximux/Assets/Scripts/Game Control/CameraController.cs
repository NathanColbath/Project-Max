using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    private GameObject mainCamera;
    public Vector3 offset = new Vector3(-17.1f,24.07f,-9.72f);

    void Start()
    {
        mainCamera = Camera.main.gameObject;
        
    }

    
    void Update()
    {
        mainCamera.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + offset;
    }

    private void LateUpdate()
    {
        
    }

  
}
