using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public bool m_IsAnimEnded { get; private set; } = false;
    public bool m_IsHideNow { get; private set; } = false;
    public bool m_IsHideEnded { get; private set; } = false;

    public void AnimEnded()
    {
        m_IsAnimEnded = true;
    }

    public void Hide()
    {
        if (!m_IsHideNow)
        {
            m_IsHideNow = true;
            GetComponent<Animator>().Play("Hide");
        }
    }

    public void HideEnded()
    {
        m_IsHideNow = false;
        m_IsHideEnded = true;
    }
}
