using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    [SerializeField] private GUIManager guiController;
    [SerializeField]
    //�@�|�[�Y�������ɕ\������UI�̃v���n�u
    private GameObject pauseUIPrefab;
    //�@�|�[�YUI�̃C���X�^���X
    private GameObject pauseUIInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (guiController.IsGameStartAnimActive()) return;
        if (Input.GetKeyDown("r"))
        {
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(pauseUIInstance);
                if (!guiController.IsTutorialActive())
                {
                    Time.timeScale = 1f;
                }
            }
        }
    }
}
