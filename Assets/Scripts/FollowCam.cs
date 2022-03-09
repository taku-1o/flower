using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    /* [SerializeField] */
    [SerializeField] private GameObject m_background;
    /* [SerializeField] */


    /* Private */
    private Flower m_flower;                                    //追従する対象
    /* Private */



    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!m_flower) return;

        transform.position = new Vector3(m_flower.transform.position.x, 0, -10);
        m_background.transform.position = new Vector3(m_flower.transform.position.x, 0, 0);
    }

    public void SetFlower(Flower flower)
    {
        m_flower = flower;
    }
}
