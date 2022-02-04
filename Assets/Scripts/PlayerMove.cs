using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //�Q�Ƃ̒�`
    private Rigidbody rb;
    public Transform mainCam;
    public GameObject bulletPrefab;
    public Transform bulletPoint;
    public AudioClip shootAudio;

    //�ϐ���`
    public float speed;
    public float upForce = 200f;
    private bool isGround;
    private readonly float posXClamp = 3.0f;
    private readonly float posYClamp = 3.0f;
    private readonly float posZClamp = 3.0f;


    void Start()
    {
        //�Q�Ƃ̐ݒ�
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        mainCam = Camera.main.transform;
    }

    void Update()
    {
        //�W�����v����
        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(new Vector3(0, upForce, 0));
        }

        //���ˏ���
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        //�L�[�̎擾
        float xMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float zMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        //�����̍X�V
        if(zMove > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, mainCam.eulerAngles.y, transform.rotation.z));
        }

        //�ʒu�̍X�V
        transform.position += transform.forward * zMove + transform.right * xMove;
        //MoveRestriction();

    }

    //�e�𔭎�
    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity) as GameObject;
        newBullet.GetComponent<Rigidbody>().AddForce(1000, 0, 0);
        GetComponent<AudioSource>().PlayOneShot(shootAudio);
    }

    //�ړ�����
    void MoveRestriction()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -posXClamp, posXClamp),
                                         Mathf.Clamp(transform.position.y, -posYClamp, posYClamp),
                                         Mathf.Clamp(transform.position.z, -posZClamp, posZClamp));
    }

    //��������
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fall"))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
