using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    public Button FirstSelectButton;
    public float StartTime;
    int seconds;

    // Start is called before the first frame update
    void Start()
    {
        FirstSelectButton.Select();

    }

    // Update is called once per frame
    void Update()
    {
        StartTime -= Time.deltaTime;
        seconds = (int)StartTime;
    }

   public void OnClick()
    {
       // if (Input.GetMouseButtonDown(0))
       // {
            //if (Input.GetKeyDown(KeyCode.Return))
            //{
               // FadeManager.Instance.LoadScene("SampleScene", 1.0f);

            //}
       // }
    }

    public void Retry()
    {
        if (StartTime < 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                MyFadeManager.Instance.LoadScene("Game", 1.0f, true);

            }
        }
    }
    public void ClickRetry()
    {
        if (StartTime < 0.0f)
        {
            MyFadeManager.Instance.LoadScene("Game", 1.0f, true);
        }
    }

        public void Title()
    {
        if (StartTime < 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                MyFadeManager.Instance.LoadScene("SampleScene", 1.0f, true);

            }
        }
       
    }

    public void Clickta()
    {
        if (StartTime < 0.0f)
        {
            MyFadeManager.Instance.LoadScene("SampleScene", 1.0f, true);
        }
    }
}
