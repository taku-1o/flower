using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [SerializeField] private Image m_hpbar;
    [SerializeField] private Image[] m_hpImages;

    public void SetHP(int maxHP, int hp)
    {
        m_hpbar.fillAmount = (float)hp / maxHP;

        for (int i = 0; i < m_hpImages.Length; i++)
        {
            m_hpImages[i].gameObject.SetActive(hp > i);
        }
    }
}
