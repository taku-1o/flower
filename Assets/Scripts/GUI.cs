using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    /* [SerializeField] */
    [SerializeField] private Image m_hpbar;
    [SerializeField] private Image[] m_hpImages;
    [SerializeField] private Text getItemText;
    /* [SerializeField] */



    public void SetItemActive(bool active)
    {
        getItemText.gameObject.SetActive(active);
    }

    public void SetHP(int maxHP, int hp)
    {
        m_hpbar.fillAmount = (float)hp / maxHP;

        for (int i = 0; i < m_hpImages.Length; i++)
        {
            m_hpImages[i].gameObject.SetActive(hp > i);
        }
    }
}
