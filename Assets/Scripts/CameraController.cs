using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //éQè∆íËã`
    public Camera mainCam;
    public Camera subCam;
    public GameObject axis;
    public GameObject player;

    void Start()
    {
        subCam.enabled = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeCam();
        }
    }

    public void ChangeCam()
    {
        if (!subCam.enabled)
        {
            mainCam.enabled = false;
            subCam.enabled = true;
            subCam.transform.rotation = Quaternion.Euler(new Vector3(axis.transform.rotation.x, axis.transform.eulerAngles.y, axis.transform.rotation.z));
        }
        else
        {
            subCam.enabled = false;
            mainCam.enabled = true;
            axis.transform.rotation = Quaternion.Euler(new Vector3(subCam.transform.rotation.x, subCam.transform.eulerAngles.y, subCam.transform.rotation.z));
        }
    }
}
