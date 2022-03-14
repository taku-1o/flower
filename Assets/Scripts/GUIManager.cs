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
    /* [SerializeField] */


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
}
