using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private GameObject background;

    private GameObject flower;

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!flower) return;

        transform.position = new Vector3(flower.transform.position.x, 0, -10);
        background.transform.position = new Vector3(flower.transform.position.x, 0, 0);
    }

    public void SetFlower(GameObject obj)
    {
        flower = obj;
    }
}
