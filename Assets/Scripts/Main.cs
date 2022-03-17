using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    /* [SerializeField] */
    [SerializeField] private GUIManager guiController;                 //UI管理（インゲーム）
    [SerializeField] private GameObject wareta_uekibatiPrefab;
    [SerializeField] private Flower flowerPrefab;               //プレイヤー（花）のPrefab
    [SerializeField] private Stage[] stagePrefabList;
    [SerializeField] private int stageCount;
    //[SerializeField] private Vector2 initPosition;
    //[SerializeField] private GameObject goalObject;             //ゴールオブジェクト
    [SerializeField] private Vector2[] goalOffset;              //ゴールアニメーションとの差
    [SerializeField] private GameObject gameOver;
    /* [SerializeField] */


    public static int stage_num = 0;
    public static bool is_first_play = true;

    /* Private */
    private FollowCam followCam;
    private Flower m_flower;                                    //プレイヤー（花）
    private bool m_IsClear;
    private bool m_IsClearEnd;
    private AudioSource m_audioSource;
    private Stage m_currentStage;
    private int m_IdxTutorial = -1;
    private bool m_IsTutorial;
    private bool m_IsGameStartAnim;

    private static bool[] m_TutorialFlgs = new bool[4];
    private static bool m_firstPlayManageFlg = true;
    /* Private */


    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        m_currentStage = Instantiate(stagePrefabList[stage_num], Vector3.zero, Quaternion.identity);

        m_flower = Instantiate(flowerPrefab, m_currentStage.m_StartPosition, Quaternion.identity);

        if (stage_num == 0)
        {
            Instantiate(wareta_uekibatiPrefab, m_currentStage.m_StartPosition + new Vector2(-1.25f, -0.15f), Quaternion.identity);
        }

        if (is_first_play && m_firstPlayManageFlg)
        {
            Time.timeScale = 0;
            m_firstPlayManageFlg = false;
            m_flower.SetFirstStartAnim();
            guiController.SetGameStartAnimActive(true);
        }
        else
        {
            is_first_play = false;
            guiController.SetHpActive(true);
            guiController.SetTimerTextActive(true);
        }
        m_flower.gameObject.name = "Flower";
        m_flower.AddHealEreaEnterEvent(ShowHealEreaTutorial);
        m_flower.AddItemEnterEvent(ShowItemTutorial);
        if (stage_num == 3) m_flower.ToggleDebugMode();


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
                if (stageCount > stage_num + 1)
                {
                    ShowNextStage();
                }
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
                }

                Vector3 finishPos = m_currentStage.m_GoalObject.transform.position - (Vector3)goalOffset[m_flower.m_selection];
                Vector3 diffPos = finishPos - m_flower.transform.position;
                if (Mathf.Abs(diffPos.x) < 0.01f && Mathf.Abs(diffPos.y) < 0.01f)
                {
                    m_flower.Finish();
                    m_currentStage.m_GoalObject.SetActive(false);
                    if (stageCount <= stage_num + 1)
                    {
                        MyFadeManager.Instance.LoadScene("GameClear", 1.2f, 0.6f, true);
                    }
                    //MyFadeManager.Instance.LoadScene("GameClear", 1.2f, 0.6f, true);
                }
                else
                {
                    m_flower.SetGoalPos(finishPos);
                }
            }

            if (m_IdxTutorial >= 0)
            {
                if (Time.timeScale != 0)
                {
                    Time.timeScale = 0;
                    guiController.SetTutorialImageActive(true, m_IdxTutorial);
                    m_TutorialFlgs[m_IdxTutorial] = true;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (m_IdxTutorial == 1)
                    {
                        m_IdxTutorial = 2;
                        m_TutorialFlgs[m_IdxTutorial] = true;
                    }
                    else
                    {
                        m_IdxTutorial = -1;
                    }
                    guiController.SetTutorialImageActive(m_IdxTutorial >= 0, m_IdxTutorial, true);
                }
            }
            if (Time.timeScale == 0)
            {
                if (m_IsGameStartAnim)
                {
                    if (guiController.IsGameStartHideEnded())
                    {
                        Time.timeScale = 1;
                        guiController.SetHpActive(true);
                        guiController.SetTimerTextActive(true);
                    }
                    else if (guiController.IsGameStartAnimEnded() && !guiController.IsGameStartHideNow())
                    {
                        guiController.SetGameStartAnimActive(false);
                    }
                }

                if (m_IsTutorial)
                {
                    Time.timeScale = guiController.IsTutorialActive() ? 0 : 1;
                }

                m_IsTutorial = guiController.IsTutorialActive();
                m_IsGameStartAnim = guiController.IsGameStartAnimActive();
            }
            else
            {
                guiController.SetHpPer(m_flower.m_timeLife / m_flower.m_limitLifeTime);
            }

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

        if (Input.GetKeyDown(KeyCode.F5))
        {
            m_flower.ToggleDebugMode();
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
            NextStage();
        }
    }

    private void ShowNextStage()
    {
        guiController.SetStageClearUIActive(true);
    }

    public void NextStage()
    {
        stage_num++;
        if (stagePrefabList.Length <= stage_num) stage_num = 0;
        MyFadeManager.Instance.LoadScene("Game", 1f, true);
    }

    public void ShowHealEreaTutorial()
    {
        if (!m_TutorialFlgs[0])
        {
            m_IdxTutorial = 0;
        }
    }

    public void ShowItemTutorial(Item item)
    {
        if (item.m_flowerSelection == 1)
        {
            if (!m_TutorialFlgs[1])
            {
                m_IdxTutorial = 1;
            }
        }
        else if (item.m_flowerSelection == 2)
        {
            if (!m_TutorialFlgs[3])
            {
                m_IdxTutorial = 3;
            }
        }
    }
}
