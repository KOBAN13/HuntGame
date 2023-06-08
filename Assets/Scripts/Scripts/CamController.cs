using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform target;
    public Transform player;

    public float rotationSpeed;

    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Calling the method for controlling the camera
        CamControl();
    }

    //CamControl() method where all of it's logic takes place
    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed; //Controlling the X - axis of the camera, to control the camera's movement on the X - axis
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed; //Controlling the y - axis of the camera, to control the camera's movement on the Y - axis
        mouseY = Mathf.Clamp(mouseY, -35, 60); //Clamping the Camera's movement on the Y - axis so that it doesn't move past those value given

        transform.LookAt(target);

        //The if statement for the camera, when you press the right mouse button. to controll if the camera should rotate with the player, or around the player.
        if(Input.GetKey(KeyCode.Mouse1))
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }
}
