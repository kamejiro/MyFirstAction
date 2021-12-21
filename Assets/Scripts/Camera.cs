using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    //�Q�Ƃ̒�`
    public GameObject player;
    //�f�t�H���g�̋���
    private Vector3 defaultDistance;

    // Start is called before the first frame update
    void Start()
    {
        //�f�t�H���g�̋������v�Z
        defaultDistance = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + defaultDistance;
    }
}
