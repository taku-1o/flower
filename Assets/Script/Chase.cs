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

    //プレイヤーが範囲に入った時のフラグ
    bool flg;

    //初期位置フラグ
    private bool startPosition;

    //ダウンしたかのフラグ
    bool Downflg;
    private GameObject player;

    public BoxCollider2D col;

    //SE再生
    private AudioSource sound01;

    //スケール計算変数
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

        // 初期位置（ゲームスタート時点の位置）
        initialPosition = transform.position;

        // 初期角度（ゲームスタート時点の角度）
        initialRot = transform.eulerAngles;

        flg = false;

        Downflg = false;

        sound01 = GetComponent<AudioSource>();

        startPosition = false;
    }

    //Update is called once per frame

    void  Update()
    {
      
      

    }

    public void OnDetectObject(Collider2D collider)
     {
        if (Downflg == false)
        {
            if (collider.CompareTag("Player"))
            {
                flg = true;

                 Animator animato = GetComponent<Animator>();
                animato.Play("hati_found");

                //sound01.PlayOneShot(sound01.clip);

                // 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
                Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;

                Animator animator = GetComponent<Animator>();
                animator.Play("hati_attack");

                float vx = dir.x * speed;
                float vy = dir.y * speed;
                rbody.velocity = new Vector2(vx, vy);

                //this.GetComponent<SpriteRenderer>().flipX = (vx < 0);
                //this.GetComponent<SpriteRenderer>().flipY = (vy < 0);

                // navMeshAgent.destination = collider.transform.position;

                // navMeshAgent.destination = player.transform.position;
                float x = Input.GetAxisRaw("Horizontal");

                // デフォルトが右向きの画像の場合
                // スケール値取り出し
                Vector2 scale = transform.localScale;

                if (vx < 0)
                {

                    // 右方向に移動中
                    scale.x = -Mathf.Abs(Scalecalculation); // そのまま（右向き


                }
                 else
                {

                    // 左方向に移動中
                    scale.x =Mathf.Abs(Scalecalculation); // 反転する（左向き）

                }
                // 代入し直す

                scale.y = Mathf.Abs(Scalecalculation); 
                transform.localScale = scale;
            }
            else
            {
                flg = false;
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


            if (flg == true) return;
           

            float x = Input.GetAxisRaw("Horizontal");

            float dist = Vector3.Distance(initialPosition, this.transform.position);
            if (dist < 0.1)
            {
                startPosition = false;
            }


            if (startPosition == true)
            {
                
                
                // 初期位置に戻す。
                Vector3 dir = (initialPosition - this.transform.position).normalized;
                dir*=0.1f;
               
                
                dir.x = Mathf.Clamp(dir.x, - dist, dist);
                dir.y = Mathf.Clamp(dir.y, -dist, dist);

                transform.Translate(dir);

            }
            else
            {
                Vector2 p = new Vector2(move, 0);
                transform.Translate(p);
                counter++;

                //countが100になれば-1を掛けて逆方向に動かす
                if (counter == 100 && flg == false)
                {

                    counter = 0;
                    move *= -1;

                }
            }
          
          



        }
        else if(Downflg==true)
        {
            float moveY = 0.01f;
            Vector2 p = new Vector2(0, -moveY);
            transform.Translate(p);
            moveY *= -1;
        }
       
    }

   public void OnTriggerEnter2D(Collider2D other)
    {
        if (Downflg == false)
        {
            if (other.CompareTag("Attack"))
            {
               

                col.enabled = false;
                Downflg = true;
                // move = 0;
              
                Destroy(gameObject, 2.5f);
                Animator animator = GetComponent<Animator>();
                animator.Play("hati_Down");
            }
        }
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
}
