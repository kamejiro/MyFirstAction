using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //参照の定義
    private Rigidbody rb;
    public GameObject bulletPrefab;
    public Transform bulletPoint;

    public float speed;
    public float upForce = 200f;
    private bool isGround;
    private readonly float posXClamp = 3.0f;
    private readonly float posYClamp = 3.0f;
    private readonly float posZClamp = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        //参照の設定
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //ジャンプ処理
        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(new Vector3(0, upForce, 0));
        }

        //発射処理
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //キーの取得
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 vector3 = new Vector3(x, 0, z);
        rb.AddForce(vector3 * speed);
        MoveRestriction();

    }

    //弾を発射
    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity) as GameObject;
        newBullet.GetComponent<Rigidbody>().AddForce(1000, 0, 0);
    }
    //移動制限
    void MoveRestriction()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -posXClamp, posXClamp),
                                         Mathf.Clamp(transform.position.y, -posYClamp, posYClamp),
                                         Mathf.Clamp(transform.position.z, -posZClamp, posZClamp));
    }
}
