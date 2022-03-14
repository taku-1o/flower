using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MyFadeManager.Instance.LoadScene("SampleScene", 1.0f, true);
        }
    }
}
