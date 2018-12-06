using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCameras_Controller : MonoBehaviour {

    public Camera FirstPersonCam, cam2, cam3, cam4, cam5, cam6;
    public KeyCode key1, key2, key3, key4, key5, key6;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(key1))
        {
            FirstPersonCam.gameObject.SetActive(true);
            cam2.gameObject.SetActive(false);
            cam3.gameObject.SetActive(false);
            cam4.gameObject.SetActive(false);
            cam5.gameObject.SetActive(false);
            cam6.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(key2))
        {
            FirstPersonCam.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
            cam3.gameObject.SetActive(false);
            cam4.gameObject.SetActive(false);
            cam5.gameObject.SetActive(false);
            cam6.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(key3))
        {
            FirstPersonCam.gameObject.SetActive(false);
            cam2.gameObject.SetActive(false);
            cam3.gameObject.SetActive(true);
            cam4.gameObject.SetActive(false);
            cam5.gameObject.SetActive(false);
            cam6.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(key4))
        {
            FirstPersonCam.gameObject.SetActive(false);
            cam2.gameObject.SetActive(false);
            cam3.gameObject.SetActive(false);
            cam4.gameObject.SetActive(true);
            cam5.gameObject.SetActive(false);
            cam6.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(key5))
        {
            FirstPersonCam.gameObject.SetActive(false);
            cam2.gameObject.SetActive(false);
            cam3.gameObject.SetActive(false);
            cam4.gameObject.SetActive(false);
            cam5.gameObject.SetActive(true);
            cam6.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(key6))
        {
            FirstPersonCam.gameObject.SetActive(false);
            cam2.gameObject.SetActive(false);
            cam3.gameObject.SetActive(false);
            cam4.gameObject.SetActive(false);
            cam5.gameObject.SetActive(false);
            cam6.gameObject.SetActive(true);
        }

    }
}
