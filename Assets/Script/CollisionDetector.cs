using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.AI;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(NavMeshAgent))]
public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();
    
    private NavMeshAgent navMeshAgent;

    /// <summary>
    /// Is TriggerがONで他のColliderと重なっているときに呼ばれ続ける
    /// </summary>
    /// <param name="other"></param>
    void Start()
    {
       
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // InspectorタブのonTriggerStayで指定された処理を実行する
       
        onTriggerStay.Invoke(other);
       

       
    }

    // UnityEventを継承したクラスに[Serializable]属性を付与することで、Inspectorウインドウ上に表示できるようになる。
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider2D>
    {

    }
    public void OnDetectObject(Collider2D collider)
    {

        //// 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        //if (collider.CompareTag("Player"))
        //{
        //    Debug.Log("a");
        //    navMeshAgent.destination = collider.transform.position;
        //    Debug.Log("a");
        //}

    }
}
