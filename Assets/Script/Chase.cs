using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Chase : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    //public string targetName;
    //public float speed = 1;

    GameObject targetObject;
    Rigidbody2D rbody;
   // Start is called before the first frame update
    void Start()
    {
        //targetObject = GameObject.Find(targetName);

       
        navMeshAgent = GetComponent<NavMeshAgent>();

        //rbody = GetComponent<Rigidbody2D>();
        //rbody.gravityScale = 0;
        //rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    //Update is called once per frame

    void Update()
    {
        //Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;

        //float vx = dir.x * speed;
        //float vy = dir.y * speed;
        //rbody.velocity = new Vector2(vx, vy);

        //this.GetComponent<SpriteRenderer>().flipX = (vx < 0);
    }

        public void OnDetectObject(Collider2D collider)
       {
   
        // 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        if (collider.CompareTag("Player"))
        {
            Debug.Log("a");
            navMeshAgent.destination = collider.transform.position;
            Debug.Log("a");
        }
    
        }
}
