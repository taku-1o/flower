using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class heal : MonoBehaviour
{
    //SEçƒê∂
    public AudioSource sound01;

    // Start is called before the first frame update
    void Start()
    {
        //sound01 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           sound01.Play();
        }
    }
}
