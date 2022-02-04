using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{

    //参照の定義
    public GameObject player;
    public Camera mainCam;

    //変数定義
    //感度
    float sensityvityX = 3;
    float sensityvityY = 3;
    //カメラの速度
    float attenuate;
    //角度制限
    float angleUp = 60f;
    float angleDown = -30f;

    void Start()
    {
        //参照の初期化
        mainCam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float xRot = Input.GetAxis("Mouse X") * sensityvityY;
        float yRot = Input.GetAxis("Mouse Y") * sensityvityX;

        transform.position = player.transform.position;
        transform.eulerAngles += new Vector3(yRot, xRot, 0);

        float angleX = transform.eulerAngles.x;
        if(angleX >= 180)
        {
            angleX -= 360;
        }
        transform.eulerAngles = new Vector3(Mathf.Clamp(angleX, angleDown, angleUp), transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
