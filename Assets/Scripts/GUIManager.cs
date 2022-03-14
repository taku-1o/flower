using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    /* [SerializeField] */
    [SerializeField] private Image m_hpImage;
    [SerializeField] private GameObject m_getUI;
    [SerializeField] private Vector2 m_getUIOffset;
    [SerializeField] private GameObject m_stageClear;
    [SerializeField] private Tutorial[] m_TutorialImages;
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
    }

    public void SetHpPer(float p)
    {
        m_hpImage.GetComponent<Animator>().SetFloat("LifeTime", Mathf.Clamp(p, 0f, 1f));
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
}
