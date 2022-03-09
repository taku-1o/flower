using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GUI guiController;
    [SerializeField] private Flower flowerPrefab;

    private Flower m_flower;

    private void Start()
    {
        m_flower = Instantiate(flowerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Camera.main.GetComponent<FollowCam>().SetFlower(m_flower);
    }

    private void Update()
    {
        if (m_flower)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                m_flower.Damage();
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                m_flower.Heal();
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                m_flower.Pick(new Item(0));
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                m_flower.Pick(new Item(1));
            }

            guiController.SetHP(m_flower.GetMaxHP(), m_flower.GetHP());
        }

        if (m_flower.GetSelection() != m_flower.GetNextSelection())
        {
            Time.timeScale = 0;
            int selection = m_flower.GetSelection();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                m_flower.Select();
            }
            return;
        }
    }

    private void FixedUpdate()
    {

    }
}
