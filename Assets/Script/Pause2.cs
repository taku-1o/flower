using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause2 : MonoBehaviour
{
    [SerializeField]
    private GameObject Menu;
    [SerializeField]
    private GameObject Panal;
  
    [SerializeField]
    private GameObject Text;
   
    [SerializeField]
    private GameObject ButtonRetry;

    [SerializeField]
    private GameObject ButtonTitle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            if (Menu == null)
            {
                Menu = GameObject.Instantiate(Menu) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(Menu);
                Time.timeScale = 1f;
            }
        }
    }
}
