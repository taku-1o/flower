using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;

public class CameraZoomin : MonoBehaviour
{
    // Start is called before the first frame update
    Transform tf; //Main Camera��Transform
    Camera cam; //Main Camera��Camera

    void Start()
    {
        tf = this.gameObject.GetComponent<Transform>(); //Main Camera��Transform���擾����B
        cam = this.gameObject.GetComponent<Camera>(); //Main Camera��Camera���擾����B
        cam.orthographicSize = cam.orthographicSize - 4.5f; //�Y�[���C���B
    }

    void Update()
    {
        //if (cam.fieldOfView == 50.0f)
        if (cam.orthographicSize<5.0f)
        {
            // cam.orthographicSize = 4.5f;
            // cam.fieldOfView = 10f;
            cam.orthographicSize = cam.orthographicSize + 0.1f;
            //cam.orthographicSize = cam.orthographicSize + 1.0f; //�Y�[���A�E�g�B

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

    //    // �ŏ��l�ƍő�l�����߂�i���R�ɕύX�\�j
    //    if (cam.fieldOfView < 10f)
    //    {
    //        cam.fieldOfView = 10f;
    //    }

    //    // �u�����̎�σJ�����v����ɐ��l�����߂Ă��������B
    //    if (cam.fieldOfView > 60f)
    //    {
    //        cam.fieldOfView = 60f;
    //    }

    //    // ���^�[���L�[�������ƁAzoom�̐��l�������i�{�^���͎��R�ɕύX�\�j
    //    if (Input.GetKey(KeyCode.Return))
    //    {
    //        // �ǂꂭ�炢�̑��x��zoom��ύX�����邩�����R�ł��B
    //        zoom -= 0.3f;

    //    } // �E�V�t�g�L�[�������ƁAzoom�̐��l�������i�{�^���͎��R�ɕύX�\�j
    //    else if (Input.GetKey(KeyCode.RightShift))
    //    {
    //        zoom += 0.3f;
    //    }
    //}
}

