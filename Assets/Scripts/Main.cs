using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GUI guiController;
    [SerializeField] private GameObject[] flowerPrefab;

    [SerializeField] private float speed;

    private GameObject flowerObj;
    private Flower flower;
    private float inputX;


    private int selection = -1;

    private void Start()
    {

    }

    private void Update()
    {
        if (flower)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                flower.Damage();
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                flower.Heal();
            }

            guiController.SetHP(flower.GetMaxHP(), flower.GetHP());
        }
        //if (selection < 0)
        //{
        if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (createFlower(0))
                {
                    selection = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (createFlower(1))
                {
                    selection = 1;
                }
            }
            //return;
        //}

        inputX = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (!flowerObj) return;

        flowerObj.transform.position += new Vector3(inputX * speed, 0, 0);
    }

    private bool createFlower(int num)
    {
        if (flowerPrefab.Length <= num) return false;

        Vector3 pos = Vector3.zero;
        if (flowerObj)
        {
            pos = flowerObj.transform.position;
            Destroy(flowerObj);
        }
        flowerObj = Instantiate(flowerPrefab[num], pos, Quaternion.identity);
        flower = flowerObj.GetComponent<Flower>();
        Camera.main.GetComponent<FollowCam>().SetFlower(flowerObj);

        return true;
    }
}
