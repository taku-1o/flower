using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    /* [SerializeField] */
    [SerializeField] private Text m_timerText;
    [SerializeField] private Image m_hpImage;
    [SerializeField] private GameObject m_getUI;
    [SerializeField] private Vector2 m_getUIOffset;
    [SerializeField] private GameObject m_stageClear;
    [SerializeField] private Tutorial[] m_TutorialImages;
    [SerializeField] private GameStart m_gameStart;
    /* [SerializeField] */


    private void Update()
    {
        for (int i = 0; i < m_TutorialImages.Length; i++)
        {
            if (m_TutorialImages[i].gameObject.activeSelf)
            {
                if (m_TutorialImages[i].m_IsTutorialHide)
                {
                    m_TutorialImages[i].gameObject.SetActive(false);
                }
            }
        }
        if (m_gameStart.gameObject.activeSelf)
        {
            if (m_gameStart.m_IsHideEnded)
            {
                m_gameStart.gameObject.SetActive(false);
            }
        }
    }

    public void SetTimerTextActive(bool active)
    {
        m_timerText.gameObject.SetActive(active);
    }

    public void SetHpPer(float p)
    {
        m_hpImage.GetComponent<Animator>().SetFloat("LifeTime", Mathf.Clamp(p, 0f, 1f));
    }

    public void SetHpActive(bool active)
    {
        m_hpImage.gameObject.SetActive(active);
    }

    public void SetGetUIActive(bool active)
    {
        SetGetUIActive(active, Vector3.zero);
    }
    public void SetGetUIActive(bool active, Vector3 pos)
    {
        m_getUI.SetActive(active);
        m_getUI.transform.position = pos + (Vector3)m_getUIOffset;
    }
    public void SetStageClearUIActive(bool active)
    {
        m_stageClear.SetActive(active);
    }

    public void SetTutorialImageActive(bool active, int idx, bool defaultStay = false)
    {
        for (int i = 0; i < m_TutorialImages.Length; i++)
        {
            if (m_TutorialImages[i].gameObject.activeSelf)
            {
                if (!active)
                {
                    m_TutorialImages[i].GetComponent<Animator>().Play("TutorialEnd");
                }
                else
                {
                    m_TutorialImages[i].gameObject.SetActive(false);
                }
            }
            else if (idx == i)
            {
                m_TutorialImages[i].gameObject.SetActive(active);
                if (defaultStay)
                {
                    m_TutorialImages[i].GetComponent<Animator>().Play("Stay");
                }
            }
        }
    }

    public bool IsTutorialActive()
    {
        for (int i = 0; i < m_TutorialImages.Length; i++)
        {
            if (m_TutorialImages[i].gameObject.activeSelf) return true;
        }
        return false;
    }

    public void SetGameStartAnimActive(bool active)
    {
        if (!m_gameStart.gameObject.activeSelf)
        {
            m_gameStart.gameObject.SetActive(active);
        }
        else if (!active)
        {
            m_gameStart.Hide();
        }
    }

    public bool IsGameStartAnimActive()
    {
        return m_gameStart.gameObject.activeSelf;
    }

    public bool IsGameStartAnimEnded()
    {
        return m_gameStart.m_IsAnimEnded || !m_gameStart.gameObject.activeSelf;
    }

    public bool IsGameStartHideNow()
    {
        return m_gameStart.m_IsHideNow;
    }

    public bool IsGameStartHideEnded()
    {
        return m_gameStart.m_IsHideEnded || !m_gameStart.gameObject.activeSelf;
    }
}
