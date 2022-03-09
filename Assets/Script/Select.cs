using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    public Button FirstSelectButton;

    // Start is called before the first frame update
    void Start()
    {
        FirstSelectButton.Select();

    }

    // Update is called once per frame
    void Update()
    {
       
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FadeManager.Instance.LoadScene("Game", 1.0f);

        }
    }

    public void Title()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FadeManager.Instance.LoadScene("SampleScene", 1.0f);

        }
    }

    //private void OnMouseDown()
    //{
    //    FadeManager.Instance.LoadScene("SampleScene", 1.0f);
    //}
}
