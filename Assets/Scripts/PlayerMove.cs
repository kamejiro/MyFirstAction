using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //参照の定義
    private Rigidbody rb;
    public Transform mainCam;
    public GameObject bulletPrefab;
    public Transform bulletPoint;
    public AudioClip shootAudio;

    //変数定義
    public float speed;
    public float upForce = 200f;
    private bool isGround;
    private readonly float posXClamp = 3.0f;
    private readonly float posYClamp = 3.0f;
    private readonly float posZClamp = 3.0f;


    void Start()
    {
        //参照の設定
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        mainCam = Camera.main.transform;
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

    void FixedUpdate()
    {
        //キーの取得
        float xMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float zMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        //向きの更新
        if(zMove > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, mainCam.eulerAngles.y, transform.rotation.z));
        }

        //位置の更新
        transform.position += transform.forward * zMove + transform.right * xMove;
        //MoveRestriction();

    }

    //弾を発射
    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity) as GameObject;
        newBullet.GetComponent<Rigidbody>().AddForce(1000, 0, 0);
        GetComponent<AudioSource>().PlayOneShot(shootAudio);
    }

    //移動制限
    void MoveRestriction()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -posXClamp, posXClamp),
                                         Mathf.Clamp(transform.position.y, -posYClamp, posYClamp),
                                         Mathf.Clamp(transform.position.z, -posZClamp, posZClamp));
    }

    //落下処理
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fall"))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
