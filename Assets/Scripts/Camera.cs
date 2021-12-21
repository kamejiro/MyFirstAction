using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    //参照の定義
    public GameObject player;
    //デフォルトの距離
    private Vector3 defaultDistance;

    // Start is called before the first frame update
    void Start()
    {
        //デフォルトの距離を計算
        defaultDistance = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + defaultDistance;
    }
}
