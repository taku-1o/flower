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

        // プレイヤータグの取得
        Player = GameObject.FindWithTag("Player");
        Target = Player.transform;

        Controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        if (InArea)
        {
            // プレイヤーのほうを向かせる
            this.transform.LookAt(Target.transform);

            // キューブとプレイヤー間の距離を計算
            Vector2 direction = Target.position - this.transform.position;
            direction = direction.normalized;

            // プレイヤー方向の速度を作成
            Vector2 velocity = direction * MoveSpeed;

            //// プレイヤーがジャンプしたときにキューブが浮かないようにy速度を0に固定しておく(空中も追従させたい場合は不要)
            //velocity.y = 0.0f;

            // キューブを動かす
            Controller.Move(velocity * Time.deltaTime);
        }

        //プレイヤーとキューブ間の距離を計算
        Vector2 Apos = this.transform.position;
        Vector2 Bpos = Target.transform.position;
        float distance = Vector2.Distance(Apos, Bpos);

        // 距離がDetecDistの設定値未満の場合は検知フラグをfalseにする。
        if (distance > DetecDist)
        {
            InArea = false;
        }
    }

    // プレイヤーが検知エリアにはいたら検知フラグをtrueにする。
    private void OnTriggerEnter(Collider other)
    {
        InArea = true;
    }
}
