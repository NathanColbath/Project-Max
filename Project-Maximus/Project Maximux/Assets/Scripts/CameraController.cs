using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public bool lockCursor = true;

    public float cameraSpeed = 120.0f;
    public GameObject player;
    
    public float clampAngle = 80.0f;
    public float sensativity;
    
    
    
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float smoothX;
    public float smoothY;
    
    private float rotY = 0.0f;
    private float rotX = 0.0f;
    private Vector3 followPosition;
    private GameObject mainCamera;

    void Start()
    {
        mainCamera = Camera.main.gameObject;

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        
    }

    
    void Update()
    {

        mouseX = Input.GetAxis("Mouse Y");
        mouseY = Input.GetAxis("Mouse X");

        rotY += mouseY * sensativity * Time.deltaTime;
        rotX += mouseX * sensativity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        //Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        //transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        updateCamera();
    }

    void updateCamera()
    {
        Transform target = player.transform;

        //Do Movement
        float step = cameraSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
