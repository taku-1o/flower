using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    /* [SerializeField] */
    [SerializeField] private GUIManager guiController;                 //UI管理（インゲーム）
    [SerializeField] private Flower flowerPrefab;               //プレイヤー（花）のPrefab
    [SerializeField] private Stage[] stagePrefabList;
    //[SerializeField] private Vector2 initPosition;
    //[SerializeField] private GameObject goalObject;             //ゴールオブジェクト
    [SerializeField] private Vector2[] goalOffset;              //ゴールアニメーションとの差
    [SerializeField] private GameObject gameOver;
    /* [SerializeField] */


    public static int stage_num = 0;

    /* Private */
    private FollowCam followCam;
    private Flower m_flower;                                    //プレイヤー（花）
    private bool m_IsClear;
    private bool m_IsClearEnd;
    private AudioSource m_audioSource;
    private Stage m_currentStage;
    /* Private */


    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        if (stagePrefabList.Length <= stage_num) stage_num = 0;

        m_currentStage = Instantiate(stagePrefabList[stage_num], Vector3.zero, Quaternion.identity);

        m_flower = Instantiate(flowerPrefab, m_currentStage.m_StartPosition, Quaternion.identity);
        m_flower.gameObject.name = "Flower";

        followCam = Camera.main.GetComponent<FollowCam>();
        followCam.SetFlower(m_flower);
        followCam.SetBackground(m_currentStage.m_background);
        followCam.SetLimitRange(m_currentStage.m_LimitRangeLeft, m_currentStage.m_LimitRangeRight);
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
                    followCam.SetZoom(true);
                    stage_num++;
                }

                Vector3 finishPos = m_currentStage.m_GoalObject.transform.position - (Vector3)goalOffset[m_flower.m_selection];
                Vector3 diffPos = finishPos - m_flower.transform.position;
                if (Mathf.Abs(diffPos.x) < 0.001f && Mathf.Abs(diffPos.y) < 0.001f)
                {
                    m_flower.Finish();
                    m_currentStage.m_GoalObject.SetActive(false);
                    MyFadeManager.Instance.LoadScene("GameClear", 1.2f, 0.6f, true);
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

            guiController.SetHpPer(m_flower.m_timeLife / m_flower.m_limitLifeTime);

            if (m_flower.m_limitLifeTime <= m_flower.m_timeLife)
            {
                m_audioSource.Stop();
                gameOver.SetActive(true);
            }

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
            stage_num++;
            MyFadeManager.Instance.LoadScene("Game", 1f);
        }
    }
}
