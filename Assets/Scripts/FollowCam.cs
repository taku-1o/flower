using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    /* [SerializeField] */
    [SerializeField] private GameObject m_background;
    [SerializeField] private float m_zoomMin;
    [SerializeField] private float m_zoomMax;
    [SerializeField] private float m_zoomTime;
    [SerializeField] private float m_zoomRange;
    /* [SerializeField] */


    /* Private */
    private Camera m_camera;
    private Flower m_flower;                                    //追従する対象
    private bool m_zoomFlg;
    private bool m_zoomEndFlg = true;
    private Vector2 m_rangeRightTop;
    private Vector2 m_rangeLeftBotom;
    /* Private */



    private void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!m_flower) return;
        if (m_zoomEndFlg) return;
        if (m_zoomFlg)
        {
            if (m_zoomMax < m_camera.orthographicSize)
            {
                m_camera.orthographicSize -= (m_zoomMin - m_zoomMax) * (Time.deltaTime / m_zoomTime);
                if (m_camera.orthographicSize <= m_zoomMax)
                {
                    m_camera.orthographicSize = m_zoomMax;
                    m_zoomEndFlg = true;
                    m_zoomFlg = false;
                }
            }
            Vector3 dif = m_flower.transform.position - transform.position;
            dif.Normalize();

            dif.z = 0;
            dif *= Vector3.Distance(m_flower.transform.position, transform.position);

            transform.position += dif * Time.deltaTime;
            m_rangeRightTop = m_camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            m_rangeLeftBotom = m_camera.ScreenToWorldPoint(Vector3.zero);
            if (m_zoomRange < m_rangeRightTop.x)
            {
                transform.position += new Vector3(m_zoomRange - m_rangeRightTop.x, 0, 0);
            }
            else if (m_rangeLeftBotom.x < -m_zoomRange)
            {
                transform.position += new Vector3(-m_zoomRange - m_rangeLeftBotom.x, 0, 0);
            }
        }
        else
        {
            if (m_camera.orthographicSize < m_zoomMin)
            {
                m_camera.orthographicSize += (m_zoomMin - m_zoomMax) * (Time.deltaTime / m_zoomTime);
                if (m_zoomMin <= m_camera.orthographicSize)
                {
                    m_camera.orthographicSize = m_zoomMin;
                    m_zoomEndFlg = true;
                }
            }
            Vector3 dif = -transform.position;
            dif.Normalize();

            dif.z = 0;
            dif *= Vector3.Distance(Vector3.zero, transform.position);

            transform.position += dif * Time.deltaTime;
            m_rangeRightTop = m_camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            m_rangeLeftBotom = m_camera.ScreenToWorldPoint(Vector3.zero);
            if (m_zoomRange < m_rangeRightTop.x)
            {
                transform.position += new Vector3(m_zoomRange - m_rangeRightTop.x, 0, 0);
            }
            else if (m_rangeLeftBotom.x < -m_zoomRange)
            {
                transform.position += new Vector3(-m_zoomRange - m_rangeLeftBotom.x, 0, 0);
            }
        }
    }

    public void SetFlower(Flower flower)
    {
        m_flower = flower;
    }

    public void SetZoom(bool zoomFlg)
    {
        m_zoomFlg = zoomFlg;
        m_zoomEndFlg = false;
    }
}
