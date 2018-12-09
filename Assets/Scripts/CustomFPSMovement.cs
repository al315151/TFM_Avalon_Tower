using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomFPSMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    float rotationSpeed = 2.5f;
    public Vector2 pastFrameMousePosition;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pastFrameMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerRotation();
    }

    void PlayerRotation()
    {
        //Based on:
        //https://gamedev.stackexchange.com/questions/136174/im-rotating-an-object-on-two-axes-so-why-does-it-keep-twisting-around-the-thir


        float rotX = Input.GetAxis("Mouse X")  * rotationSpeed;
        float rotY = Input.GetAxis("Mouse Y")  * rotationSpeed;
        pastFrameMousePosition = Input.mousePosition;


        Quaternion yaw = Quaternion.Euler(0f, rotX, 0f);
        transform.rotation = yaw * transform.rotation;
        Quaternion pitch = Quaternion.Euler(-rotY, 0f, 0f);
        transform.rotation = transform.rotation * pitch;
    }

    void PlayerMovement()
    {
        Vector3 wantedForward = new Vector3(transform.forward.x, 0f, transform.forward.z);
        Vector3 wantedRight = new Vector3(transform.right.x, 0f, transform.right.z);


        transform.position += wantedRight * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        transform.position += wantedForward * Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
    }





}
