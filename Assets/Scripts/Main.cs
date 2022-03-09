using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    /* [SerializeField] */
    [SerializeField] private GUI guiController;                 //UI管理（インゲーム）
    [SerializeField] private Flower flowerPrefab;               //プレイヤー（花）のPrefab
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

            guiController.SetHP(m_flower.m_maxHP, m_flower.m_hp);
        }

        if (m_flower.m_selection != m_flower.m_nextSelection)
        {
            Time.timeScale = 0;
            guiController.SetItemActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                guiController.SetItemActive(false);
                Time.timeScale = 1;
                m_flower.Select();
            }
            return;
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
    }
}
