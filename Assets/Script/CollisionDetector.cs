using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Collider))]
public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();

    /// <summary>
    /// Is TriggerがONで他のColliderと重なっているときに呼ばれ続ける
    /// </summary>
    /// <param name="other"></param>
     void Start()
    {
       
    }
    private void OnTriggerStay(Collider other)
    {
        // InspectorタブのonTriggerStayで指定された処理を実行する
        Debug.Log("a");
        onTriggerStay.Invoke(other);
        Debug.Log("a");
    }

    // UnityEventを継承したクラスに[Serializable]属性を付与することで、Inspectorウインドウ上に表示できるようになる。
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {

    }
}
