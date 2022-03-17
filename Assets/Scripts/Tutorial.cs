using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool m_IsTutorialHide { get; private set; }

    private void OnEnable()
    {
        m_IsTutorialHide = false;
    }

    public void TutorialHide()
    {
        m_IsTutorialHide = true;
    }
}
