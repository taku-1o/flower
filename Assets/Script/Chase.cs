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

    bool flg;
    private GameObject player;

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
    }

    //Update is called once per frame

    void  Update()
    {
       
     

        //Vector2 dir = (targetObject.transform.position - this.transform.position).normalized;

        //float vx = dir.x * speed;
        //float vy = dir.y * speed;
        //rbody.velocity = new Vector2(vx, vy);

        //this.GetComponent<SpriteRenderer>().flipX = (vx < 0);

    }

    public void OnDetectObject(Collider2D collider)
     {
        
        if (collider.CompareTag("Player") )
        {
            flg = true;
            // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
            Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;

            float vx = dir.x * speed;
            float vy = dir.y * speed;
            rbody.velocity = new Vector2(vx, vy);

            //this.GetComponent<SpriteRenderer>().flipX = (vx < 0);
            //this.GetComponent<SpriteRenderer>().flipY = (vy < 0);

            // navMeshAgent.destination = collider.transform.position;

            // navMeshAgent.destination = player.transform.position;
            float x = Input.GetAxisRaw("Horizontal");
            // �f�t�H���g���E�����̉摜�̏ꍇ
            // �X�P�[���l���o��
            Vector2 scale = transform.localScale;

            if (vx >= 0)
            {

                // �E�����Ɉړ���
                scale.x = 1; // ���̂܂܁i�E����

            }
            else
            {

                // �������Ɉړ���
                scale.x = -1; // ���]����i�������j

            }
            // ���������
            transform.localScale = scale;
        }
        else
        {
            flg = false;
        }
    }

    public void OnDetectObj(Collider2D collider)
    {

        //if (collider.CompareTag("Player"))
        //{
        //    flg = true;
        //    // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
        //    Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;

        //    float vx = dir.x * speed;
        //    float vy = dir.y * speed;
           

        //    //this.GetComponent<SpriteRenderer>().flipX = (vx < 0);
        //    //this.GetComponent<SpriteRenderer>().flipY = (vy < 0);

        //    // navMeshAgent.destination = collider.transform.position;

        //    // navMeshAgent.destination = player.transform.position;
        //}

        if (collider.CompareTag("Player"))
        {
            flg = false;
            rbody.velocity = new Vector2(0, 0);

            // �����ʒu�ɖ߂��B
            transform.position = initialPosition;

            // �����p�x�ɖ߂��B
            transform.eulerAngles = initialRot;
        }
    }


    void FixedUpdate()
    {
        currentPosition = transform.position;

        //if (-5f > initialPosition.y || -5f > initialPosition.x)
        //{
        //    // �����ʒu�ɖ߂��B
        //    transform.position = initialPosition;

        //    // �����p�x�ɖ߂��B
        //    transform.eulerAngles = initialRot;
        //}

        if (flg == true) return;
        Vector2 p = new Vector2(move, 0);
        transform.Translate(p);
        counter++;

        //count��100�ɂȂ��-1���|���ċt�����ɓ�����
        if (counter == 100 && flg == false)
        {

            counter = 0;
            move *= -1;

        }

        float x = Input.GetAxisRaw("Horizontal");
        // �f�t�H���g���E�����̉摜�̏ꍇ
        // �X�P�[���l���o��
        Vector2 scale = transform.localScale;
        
        if (move >= 0)
        {
        
            // �E�����Ɉړ���
            scale.x = 1; // ���̂܂܁i�E����
          
        }
        else
        {

            // �������Ɉړ���
            scale.x = -1; // ���]����i�������j
            
        }
        // ���������
        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            move = 0;
            Destroy(gameObject,2.5f);
            Animator animator = GetComponent<Animator>();
            Debug.Log("a");
            animator.Play("hati_Down");
        }
    }
}