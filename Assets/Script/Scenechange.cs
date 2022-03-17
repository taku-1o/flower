using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenechange : MonoBehaviour
{

    public float totalTime;
    int seconds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        seconds = (int)totalTime;

        if(seconds==0)
        {
            MyFadeManager.Instance.LoadScene("Game", 1.0f, true);
        }
    }
}
