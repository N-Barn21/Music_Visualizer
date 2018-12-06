using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController charCont;
    public Camera playerCam;
    public bool useGravity;
    public KeyCode keyG, keySpace, keyShift;
    private float currentY = 0;
    private float maxY = 150;


    // Use this for initialization
    void Start () {
        charCont = GetComponent<CharacterController>();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(keySpace))
        {
            if (currentY <= maxY)
            {
                currentY++;
            }
        }
        else if (Input.GetKeyDown(keyShift))
        {
            if (currentY >= 0)
            {
                currentY--;
            }
        }
        else if (Input.GetKeyDown(keyG))
        {
            useGravity = !useGravity;
            currentY = 0;
        }

        if (playerCam.isActiveAndEnabled)
        {
            if (useGravity)
            {
                float deltaX = Input.GetAxis("Horizontal") * speed;
                float deltaZ = Input.GetAxis("Vertical") * speed;
                Vector3 movement = new Vector3(deltaX, 0, deltaZ);
                movement = Vector3.ClampMagnitude(movement, speed); // Limit Player Speed
                movement.y = gravity * speed;
                movement *= Time.deltaTime; //Reinforces Speed Restrictions based on Frame Rate
                movement = transform.TransformDirection(movement);
                charCont.Move(movement);
            }
            else
            {
                float deltaX = Input.GetAxis("Horizontal") * speed;
                float deltaZ = Input.GetAxis("Vertical") * speed;
                Vector3 movement = new Vector3(deltaX, 0, deltaZ);
                movement = Vector3.ClampMagnitude(movement, speed); // Limit Player Speed
                if((currentY * speed) <= maxY)
                {
                   movement.y = currentY * speed;
                }
                else
                {
                    movement.y = 0f;
                }
                movement *= Time.deltaTime; //Reinforces Speed Restrictions based on Frame Rate
                movement = transform.TransformDirection(movement);
                charCont.Move(movement);
            }
        }
    }
}
