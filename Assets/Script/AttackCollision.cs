using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider2D> enterEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        enterEvent.Invoke(collision);
    }
}
