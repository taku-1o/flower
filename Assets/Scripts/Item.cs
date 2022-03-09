using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    /* [SerializeField] */
    [SerializeField] private int flowerSelection;
    /* [SerializeField] */



    public int m_flowerSelection 
    { 
        get { return flowerSelection; } 
        private set { flowerSelection = value; } 
    }

    public Item(int selection)
    {
        m_flowerSelection = selection;
    }
}
