using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private GameObject goalObject;
    [SerializeField] private GameObject background;
    [SerializeField] private float limitRangeLeft;
    [SerializeField] private float limitRangeRight;

    public Vector2 m_StartPosition { get { return startPosition; } }
    public GameObject m_GoalObject { get { return goalObject; } }

    public float m_LimitRangeLeft { get { return limitRangeLeft; } }
    public float m_LimitRangeRight { get { return limitRangeRight; } }
    public GameObject m_background { get { return background; } }
}
