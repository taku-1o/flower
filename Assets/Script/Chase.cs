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

    //ダウンしたかのフラグ
    bool Downflg;
    private GameObject player;

    public BoxCollider2D col;
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
        if (Downflg == false)
        {
            if (collider.CompareTag("Player"))
            {
                flg = true;
                // 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
                Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;

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

                if (vx >= 0)
                {

                    // 右方向に移動中
                    scale.x = 0.6f; // そのまま（右向き

                }
                else
                {

                    // 左方向に移動中
                    scale.x = -0.6f; // 反転する（左向き）

                }
                // 代入し直す
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

       

        if (collider.CompareTag("Player"))
        {
            flg = false;
            rbody.velocity = new Vector2(0, 0);

            // 初期位置に戻す。
            transform.position = initialPosition;

            // 初期角度に戻す。
            transform.eulerAngles = initialRot;
        }
    }


    void FixedUpdate()
    {
        if (Downflg == false)
        {
            currentPosition = transform.position;

            //if (-5f > initialPosition.y || -5f > initialPosition.x)
            //{
            //    // 初期位置に戻す。
            //    transform.position = initialPosition;

            //    // 初期角度に戻す。
            //    transform.eulerAngles = initialRot;
            //}

            if (flg == true) return;
            Vector2 p = new Vector2(move, 0);
            transform.Translate(p);
            counter++;

            //countが100になれば-1を掛けて逆方向に動かす
            if (counter == 100 && flg == false)
            {

                counter = 0;
                move *= -1;

            }

            float x = Input.GetAxisRaw("Horizontal");
            // デフォルトが右向きの画像の場合
            // スケール値取り出し
            Vector2 scale = transform.localScale;

            //if (move >= 0)
            //{

            //    // 右方向に移動中
            //    scale.x = 0.6f; // そのまま（右向き

            //}
            //else
            //{

            //    // 左方向に移動中
            //    scale.x = -0.6f; // 反転する（左向き）

            //}
            // 代入し直す
            transform.localScale = scale;
            //Vector3 scale = transform.localScale;
            if (move < 0) scale.x = -Mathf.Abs(scale.x);
            if (move > 0) scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

   public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            col.enabled = false;
            Downflg = true;
            move = 0;
            Destroy(gameObject,2.5f);
            Animator animator = GetComponent<Animator>();
            animator.Play("hati_Down");
        }
    }
}
