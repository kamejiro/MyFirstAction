using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //参照の定義
    private Rigidbody rb;
    public Transform mainCam;
    public Camera subCam;
    public GameObject bulletPrefab;
    public Transform bulletPoint;
    public AudioClip shootAudio;

    //変数定義
    public float speed;
    public float upForce = 200f;
    private bool isGround;
    private readonly float posXClamp = 3.0f;//移動制限
    private readonly float posYClamp = 3.0f;
    private readonly float posZClamp = 3.0f;
    float minX = -90f, maxX = 90f;//角度制限
    float sensityvityX = 3;//感度
    float sensityvityY = 3;

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

        //一人称カメラの場合
        if (subCam.enabled)
        {
            //マウスの入力
            float xRot = Input.GetAxis("Mouse X") * sensityvityY;
            float yRot = Input.GetAxis("Mouse Y") * sensityvityX;
            
            //マウスの反映
            subCam.transform.rotation *= Quaternion.Euler(yRot, 0, 0);
            transform.rotation *= Quaternion.Euler(0, xRot, 0);
            //角度制限
            subCam.transform.rotation = ClampRotation(subCam.transform.rotation);
        }
    }

    void FixedUpdate()
    {
        //キーの取得
        float xMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float zMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        //向きの更新
        if (zMove > 0)
        {
            if (subCam.enabled)
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.eulerAngles.y, transform.rotation.z));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, mainCam.eulerAngles.y, transform.rotation.z));
            }
        }

        //位置の更新
        transform.position += transform.forward * zMove + transform.right * xMove;
        //MoveRestriction();
    }

    //弾を発射
    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity) as GameObject;
        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
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

    //角度制限する関数(オイラー角に直し、xのみClampしてまたQuaternionに直す。)
    public Quaternion ClampRotation(Quaternion q)
    {
        //wで割る
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;
        //オイラーに直す
        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;
        //クランプする
        angleX = Mathf.Clamp(angleX, minX, maxX);
        //元に戻す
        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
