using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Chase : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public string targetName;
    public float speed = 1;

    GameObject targetObject;
    Rigidbody2D rbody;

    int counter = 0;
    float move = 0.05f;

    private Vector3 initialPosition;
    private Vector3 currentPosition;
    private Vector3 initialRot;

    //�v���C���[���͈͂ɓ��������̃t���O
    bool flg;

    //�����ʒu�t���O
    private bool startPosition;


    //found���ǂ���
    bool foundflg;

    //�_�E���������̃t���O
    bool Downflg;
    private GameObject player;

    public BoxCollider2D col;

    //SE�Đ�
    private AudioSource sound01;

    public AudioSource foundSE;//����������SE

    //�X�P�[���v�Z�ϐ�
    public float Scalecalculation;

    //
    public Transform other;

    // Start is called before the first frame update
    void Start()
    {
        targetObject = GameObject.Find(targetName);
        int counter = 0;

        navMeshAgent = GetComponent<NavMeshAgent>();

        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        // �����ʒu�i�Q�[���X�^�[�g���_�̈ʒu�j
        initialPosition = transform.position;

        // �����p�x�i�Q�[���X�^�[�g���_�̊p�x�j
        initialRot = transform.eulerAngles;

        flg = false;

        Downflg = false;

        sound01 = GetComponent<AudioSource>();

        startPosition = false;

        foundflg = false;
    }

    //Update is called once per frame

    void Update()
    {

        if (targetObject)
        {
            float distance = Vector3.Distance(targetObject.transform.position, transform.position);
            Debug.Log("distance:" + distance);
            if (distance > 9)
            {
                distance = 9;
            }
            distance = 9f - distance;
            sound01.volume = 0.1f * (distance / 9f);
        }

    }

    public void OnDetectObject(Collider2D collider)
    {
        if (Downflg == false)
        {
            if (collider.CompareTag("Player"))
            {


                Animator animator = GetComponent<Animator>();

                if (foundflg == false)
                {
                    if (flg == false)
                    {
                        foundflg = true;
                        foundSE.Play();
                        animator.Play("hati_found");
                    }
                }
                flg = true;


                //else
                //{
                //    flg = false;

                //}

            }
        }
    }

    public void OnDetectObj(Collider2D collider)
    {
        if (Downflg == false)
        {
            if (collider.CompareTag("Player"))
            {
                flg = false;
                Animator animator = GetComponent<Animator>();
                animator.Play("hati");
                foundflg = false;
                rbody.velocity = new Vector2(0, 0);
                startPosition = true;

            }
        }
    }


    void FixedUpdate()
    {
        if (Downflg == false)
        {
            currentPosition = transform.position;


            if (flg == true)
            {
                if (foundflg == false)
                {
                    //sound01.PlayOneShot(sound01.clip);

                    // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
                    Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;

                    //animator = GetComponent<Animator>();
                    //animator.Play("hati_attack");

                    float vx = dir.x * speed;
                    float vy = dir.y * speed;
                    rbody.velocity = new Vector2(vx, vy);
                   // float x = Input.GetAxisRaw("Horizontal");

                    // �f�t�H���g���E�����̉摜�̏ꍇ
                    // �X�P�[���l���o��
                    Vector2 scale = transform.localScale;

                    if (vx < 0)
                    {

                        // �E�����Ɉړ���
                        scale.x = -Mathf.Abs(Scalecalculation); // ���̂܂܁i�E����


                    }
                    else
                    {

                        // �������Ɉړ���
                        scale.x = Mathf.Abs(Scalecalculation); // ���]����i�������j

                    }
                    // ���������

                    scale.y = Mathf.Abs(Scalecalculation);
                    transform.localScale = scale;
                }

                return;
            }

            float x = Input.GetAxisRaw("Horizontal");

            float dist = Vector3.Distance(initialPosition, this.transform.position);
            if (dist < 0.1)
            {
                startPosition = false;
            }


            if (startPosition == true)
            {


                // �����ʒu�ɖ߂��B
                Vector3 dir = (initialPosition - this.transform.position).normalized;
                dir *= 0.1f;


                dir.x = Mathf.Clamp(dir.x, -dist, dist);
                dir.y = Mathf.Clamp(dir.y, -dist, dist);

                transform.Translate(dir);

            }
            else
            {
                Vector2 p = new Vector2(move, 0);
                transform.Translate(p);
                counter++;

                //count��100�ɂȂ��-1���|���ċt�����ɓ�����
                if (counter == 100 && flg == false)
                {

                    counter = 0;
                    move *= -1;

                }
            }

        }
        else if (Downflg == true)
        {
            float moveY = 0.01f;
            Vector2 p = new Vector2(0, -moveY);
            transform.Translate(p);
            moveY *= -1;
        }
           
    }

    public void BodyTriggerEnter(Collider2D other)
    {
        if (Downflg == false)
        {
            if (other.CompareTag("Attack"))
            {


                col.enabled = false;
                Downflg = true;
                //move = 0;

                Destroy(gameObject, 2.5f);
                Animator animator = GetComponent<Animator>();
                animator.Play("hati_Down");
            }
        }
    }
    

    public void OnTriggerEnter2D(Collider2D other)
    {
    }

    void LateUpdate()
    {
        Vector2 scale = transform.localScale;
        if (move < 0)
        {
            scale.x = -Mathf.Abs(Scalecalculation);

        }


        if (move > 0)
        {
            scale.x = Mathf.Abs(Scalecalculation);

        }

        scale.y = Mathf.Abs(Scalecalculation);
        transform.localScale = scale;
    }


    public void FoundAnimEnd()
    {
        foundflg = false;
    }

}
