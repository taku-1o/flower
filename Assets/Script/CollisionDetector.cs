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
    /// Is Trigger��ON�ő���Collider�Əd�Ȃ��Ă���Ƃ��ɌĂ΂ꑱ����
    /// </summary>
    /// <param name="other"></param>
    void Start()
    {
       
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // Inspector�^�u��onTriggerStay�Ŏw�肳�ꂽ���������s����
       
        onTriggerStay.Invoke(other);
       

       
    }

    // UnityEvent���p�������N���X��[Serializable]������t�^���邱�ƂŁAInspector�E�C���h�E��ɕ\���ł���悤�ɂȂ�B
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider2D>
    {

    }
    public void OnDetectObject(Collider2D collider)
    {

        //// ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
        //if (collider.CompareTag("Player"))
        //{
        //    Debug.Log("a");
        //    navMeshAgent.destination = collider.transform.position;
        //    Debug.Log("a");
        //}

    }
}
