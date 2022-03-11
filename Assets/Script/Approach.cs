using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approach : MonoBehaviour
{
    CharacterController Controller;
    Transform Target;
    GameObject Player;

    [SerializeField]
    float MoveSpeed = 2.0f;
    int DetecDist = 8;
    bool InArea = false;


    // Use this for initialization
    void Start()
    {

        // �v���C���[�^�O�̎擾
        Player = GameObject.FindWithTag("Player");
        Target = Player.transform;

        Controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        if (InArea)
        {
            // �v���C���[�̂ق�����������
            this.transform.LookAt(Target.transform);

            // �L���[�u�ƃv���C���[�Ԃ̋������v�Z
            Vector2 direction = Target.position - this.transform.position;
            direction = direction.normalized;

            // �v���C���[�����̑��x���쐬
            Vector2 velocity = direction * MoveSpeed;

            //// �v���C���[���W�����v�����Ƃ��ɃL���[�u�������Ȃ��悤��y���x��0�ɌŒ肵�Ă���(�󒆂��Ǐ]���������ꍇ�͕s�v)
            //velocity.y = 0.0f;

            // �L���[�u�𓮂���
            Controller.Move(velocity * Time.deltaTime);
        }

        //�v���C���[�ƃL���[�u�Ԃ̋������v�Z
        Vector2 Apos = this.transform.position;
        Vector2 Bpos = Target.transform.position;
        float distance = Vector2.Distance(Apos, Bpos);

        // ������DetecDist�̐ݒ�l�����̏ꍇ�͌��m�t���O��false�ɂ���B
        if (distance > DetecDist)
        {
            InArea = false;
        }
    }

    // �v���C���[�����m�G���A�ɂ͂����猟�m�t���O��true�ɂ���B
    private void OnTriggerEnter(Collider other)
    {
        InArea = true;
    }
}
