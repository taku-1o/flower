using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    /* [SerializeField] */
    [SerializeField] private GUIManager guiController;                 //UI管理（インゲーム）
    [SerializeField] private Flower flowerPrefab;               //プレイヤー（花）のPrefab
    [SerializeField] private Vector2 initPosition;
    [SerializeField] private GameObject goalObject;             //ゴールオブジェクト
    [SerializeField] private Vector2[] goalOffset;              //ゴールアニメーションとの差
    /* [SerializeField] */


    /* Private */
    private Flower m_flower;                                    //プレイヤー（花）
    private bool m_IsClear;
    private bool m_IsClearEnd;
    private AudioSource m_audioSource;
    /* Private */



    private void Start()
    {
        m_flower = Instantiate(flowerPrefab, initPosition, Quaternion.identity);
        Camera.main.GetComponent<FollowCam>().SetFlower(m_flower);
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (m_flower)
        {
            /* DEBUG */
            DebugUpdate();
            /* DEBUG */

            if (m_flower.m_isGoalEnd && !m_IsClearEnd)
            {
                m_IsClearEnd = true;
                SceneManager.LoadScene("GameClear");
            }

            if (m_flower.m_isFinish && m_audioSource.volume > 0)
            {
                float nVol = m_audioSource.volume - (0.9f * (Time.deltaTime / 2.0f));
                if (nVol < 0.1f)
                {
                    nVol = 0;
                }
                m_audioSource.volume = nVol;
            }

            if (m_flower.m_isGoal && !m_flower.m_isFinish)
            {
                if (!m_IsClear)
                {
                    m_IsClear = true;
                    Camera.main.GetComponent<FollowCam>().SetZoom(true);
                }

                Vector3 finishPos = goalObject.transform.position - (Vector3)goalOffset[m_flower.m_selection];
                Vector3 diffPos = finishPos - m_flower.transform.position;
                if (Mathf.Abs(diffPos.x) < 0.001f && Mathf.Abs(diffPos.y) < 0.001f)
                {
                    m_flower.Finish();
                    goalObject.SetActive(false);
                }
                else
                {
                    Vector3 vec = Vector3.zero;
                    if (diffPos.x < -0.01f)
                    {
                        vec.x = -0.2f;
                    }
                    else if (diffPos.x > 0.01f)
                    {
                        vec.x = 0.2f;
                    }
                    else if (diffPos.x < -0.001f)
                    {
                        vec.x = -0.01f;
                    }
                    else if (diffPos.x > 0.001f)
                    {
                        vec.x = 0.01f;
                    }
                    if (diffPos.y < -0.01f)
                    {
                        vec.y = -0.2f;
                    }
                    else if (diffPos.y > 0.01f)
                    {
                        vec.y = 0.2f;
                    }
                    else if (diffPos.y < -0.001f)
                    {
                        vec.y = -0.01f;
                    }
                    else if (diffPos.y > 0.001f)
                    {
                        vec.y = 0.01f;
                    }
                    m_flower.SetInput(vec);
                }
            }

            guiController.SetHpPer(m_flower.m_timeLife / m_flower.m_lifeTime);

            if (m_flower.IsTriggerItem())
            {
                guiController.SetGetUIActive(true, m_flower.transform.position);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_flower.Pick();
                }
            }
            else
            {
                guiController.SetGetUIActive(false);
            }

            if (m_flower.m_selection != m_flower.m_nextSelection)
            {
                if (!m_flower.IsGet())
                {
                    m_flower.Select();
                }
                return;
            }
        }
    }

    private void FixedUpdate()
    {

    }

    private void DebugUpdate() //Debug
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            m_flower.Damage();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            m_flower.Heal();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            m_flower.Pick(new Item(0));
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            m_flower.Pick(new Item(1));
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            MyFadeManager.Instance.LoadScene("Game", 1f, 1f, true);
        }
    }
}
