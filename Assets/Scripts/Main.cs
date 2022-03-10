using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    /* [SerializeField] */
    [SerializeField] private GUI guiController;                 //UI管理（インゲーム）
    [SerializeField] private Flower flowerPrefab;               //プレイヤー（花）のPrefab
    [SerializeField] private GameObject goalObject;             //ゴールオブジェクト
    [SerializeField] private Vector2[] goalOffset;              //ゴールアニメーションとの差
    /* [SerializeField] */


    /* Private */
    private Flower m_flower;                                    //プレイヤー（花）
    /* Private */



    private void Start()
    {
        m_flower = Instantiate(flowerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Camera.main.GetComponent<FollowCam>().SetFlower(m_flower);
    }

    private void Update()
    {
        if (m_flower)
        {
            /* DEBUG */
            DebugUpdate();
            /* DEBUG */

            if (m_flower.m_isGoal && !m_flower.m_isFinish)
            {
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

        if (Input.GetKeyDown(KeyCode.F7))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}
