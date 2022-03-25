using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public GameObject gameOver;
    public Text timerText;
    public float totalTime;
    public float StartTime;
    int seconds;


    // Use this for initialization
    void Start()
    {
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartTime -= Time.deltaTime;
        seconds = (int)StartTime;

        if (StartTime < 0.0f)
        {
            totalTime -= Time.deltaTime;
            seconds = (int)totalTime;
            timerText.text = seconds.ToString();

           
        }

        if (totalTime < 0)
        {
            //Debug.Log("gameover");
            StartCoroutine("GameOver");

        }
      
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }
}
