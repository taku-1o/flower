using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Enter : MonoBehaviour
{
   // public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
           MyFadeManager.Instance.LoadScene("Gamestart", 1.0f);
            //SceneManager.LoadScene(sceneName);
        }
    }

  //void OnMouseDown()
  //  {
  //      SceneManager.LoadScene(sceneName);
  //  }

}
