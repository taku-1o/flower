using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log("Click");
        if (Input.GetKeyDown(KeyCode.Return))
        {
            MyFadeManager.Instance.LoadScene("Game", 1.0f,true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            MyFadeManager.Instance.LoadScene("Game", 1.0f,true);
        }
    }

    public void OnClick()
    {
       
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    Debug.Log("Click");
        //    FadeManager.Instance.LoadScene("Gameclear", 1.0f);
        //    //SceneManager.LoadScene(sceneName);
        //}
    }
}
