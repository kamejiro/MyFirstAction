using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //�Q�Ƃ̒�`
    private Rigidbody rb;
    public Transform mainCam;
    public Camera subCam;
    public GameObject bulletPrefab;
    public Transform bulletPoint;
    public AudioClip shootAudio;

    //�ϐ���`
    public float speed;
    public float upForce = 200f;
    private bool isGround;
    private readonly float posXClamp = 3.0f;//�ړ�����
    private readonly float posYClamp = 3.0f;
    private readonly float posZClamp = 3.0f;
    float minX = -90f, maxX = 90f;//�p�x����
    float sensityvityX = 3;//���x
    float sensityvityY = 3;

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

        //��l�̃J�����̏ꍇ
        if (subCam.enabled)
        {
            //�}�E�X�̓���
            float xRot = Input.GetAxis("Mouse X") * sensityvityY;
            float yRot = Input.GetAxis("Mouse Y") * sensityvityX;
            
            //�}�E�X�̔��f
            subCam.transform.rotation *= Quaternion.Euler(yRot, 0, 0);
            transform.rotation *= Quaternion.Euler(0, xRot, 0);
            //�p�x����
            subCam.transform.rotation = ClampRotation(subCam.transform.rotation);
        }
    }

    void FixedUpdate()
    {
        //�L�[�̎擾
        float xMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float zMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        //�����̍X�V
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

        //�ʒu�̍X�V
        transform.position += transform.forward * zMove + transform.right * xMove;
        //MoveRestriction();
    }

    //�e�𔭎�
    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity) as GameObject;
        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
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

    //�p�x��������֐�(�I�C���[�p�ɒ����Ax�̂�Clamp���Ă܂�Quaternion�ɒ����B)
    public Quaternion ClampRotation(Quaternion q)
    {
        //w�Ŋ���
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;
        //�I�C���[�ɒ���
        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;
        //�N�����v����
        angleX = Mathf.Clamp(angleX, minX, maxX);
        //���ɖ߂�
        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
