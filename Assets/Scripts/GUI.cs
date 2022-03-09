using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [SerializeField] private Image hpbar;
    [SerializeField] private Image[] hpImages;

    public void SetHP(int maxHP, int hp)
    {
        hpbar.fillAmount = (float)hp / maxHP;

        for (int i = 0; i < hpImages.Length; i++)
        {
            hpImages[i].gameObject.SetActive(hp > i);
        }
    }
}
