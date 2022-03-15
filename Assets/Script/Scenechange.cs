using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenechange : MonoBehaviour
{

    public float totalTime;
    float seconds;

    // Start is called before the first frame update
    void Start()
    {
        seconds = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        //totalTime -= Time.deltaTime;
        //seconds = (int)totalTime;
        if (seconds > 0)
        {
            seconds -= Time.deltaTime;
        }

        if (seconds<=0)
        {
            MyFadeManager.Instance.LoadScene("Game", 1.0f, true);
        }
    }
}
