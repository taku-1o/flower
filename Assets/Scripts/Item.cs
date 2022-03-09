using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int m_flowerSelection;

    public Item(int selection)
    {
        m_flowerSelection = selection;
    }

    public int GetFlowerSelection()
    {
        return m_flowerSelection;
    }
}
