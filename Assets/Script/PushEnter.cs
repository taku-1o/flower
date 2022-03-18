using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEnter: MonoBehaviour
{
    public GameObject EnterObject;
    //public float totalTime;
    //public float StartTime;
    //int seconds;


    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        EnterObject.SetActive(false);
        StartCoroutine(EnterAppear());
    }

    IEnumerator EnterAppear()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        EnterObject.SetActive(true);
        //StartCoroutine("CubeAppear");
    }
}
