using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;

public class CameraZoomin : MonoBehaviour
{
    // Start is called before the first frame update
    Transform tf; //Main CameraのTransform
    Camera cam; //Main CameraのCamera

    void Start()
    {
        tf = this.gameObject.GetComponent<Transform>(); //Main CameraのTransformを取得する。
        cam = this.gameObject.GetComponent<Camera>(); //Main CameraのCameraを取得する。
        cam.orthographicSize = cam.orthographicSize - 4.5f; //ズームイン。
    }

    void Update()
    {
        //if (cam.fieldOfView == 50.0f)
        if (cam.orthographicSize<5.0f)
        {
            // cam.orthographicSize = 4.5f;
            // cam.fieldOfView = 10f;
            cam.orthographicSize = cam.orthographicSize + 0.1f;
            //cam.orthographicSize = cam.orthographicSize + 1.0f; //ズームアウト。

            //}


        }
    }
    //private Camera cam;
    //private float zoom;
    //private float view;

    //void Start()
    //{
    //    cam = GetComponent<Camera>();
    //    view = cam.fieldOfView.;
    //}

    //void Update()
    //{
    //    cam.fieldOfView = view + zoom;

    //    // 最小値と最大値を決める（自由に変更可能）
    //    if (cam.fieldOfView < 10f)
    //    {
    //        cam.fieldOfView = 10f;
    //    }

    //    // 「自分の主観カメラ」を基準に数値を決めてください。
    //    if (cam.fieldOfView > 60f)
    //    {
    //        cam.fieldOfView = 60f;
    //    }

    //    // リターンキーを押すと、zoomの数値が減少（ボタンは自由に変更可能）
    //    if (Input.GetKey(KeyCode.Return))
    //    {
    //        // どれくらいの速度でzoomを変更させるかも自由です。
    //        zoom -= 0.3f;

    //    } // 右シフトキーを押すと、zoomの数値が増加（ボタンは自由に変更可能）
    //    else if (Input.GetKey(KeyCode.RightShift))
    //    {
    //        zoom += 0.3f;
    //    }
    //}
}

