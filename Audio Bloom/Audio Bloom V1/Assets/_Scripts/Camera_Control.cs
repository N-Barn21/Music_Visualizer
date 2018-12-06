using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{

    public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    };

    public RotationAxis axes = RotationAxis.MouseX;
    public float minimumVertical = -45.0f;
    public float maximumVertical = 45.0f;
    public float senseHorizontal = 10.0f;
    public float senseVertical = 10.0f;
    public float rotationX = 0;

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * senseHorizontal, 0);
        }
        else if (axes == RotationAxis.MouseY)
        {
            rotationX -= Input.GetAxis("Mouse Y") * senseVertical;
            rotationX = Mathf.Clamp(rotationX, minimumVertical, maximumVertical);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }
}