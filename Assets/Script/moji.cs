using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class moji : MonoBehaviour
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
        totalTime -= Time.deltaTime;
        Animator animator = GetComponent<Animator>();
        if (seconds <= 0)
        {
            animator.Play("moji1");
        }
       //if(seconds<=-5.5)
       // {
       //     //animator = GetComponent<Animator>();
       //     animator.Play("moji2");
       // }
       // if (seconds <=-13)
       // {
       //    // animator = GetComponent<Animator>();
       //     animator.Play("moji3");
       // }
    }

  
}
