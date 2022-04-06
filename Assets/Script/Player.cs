using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed; //プレイヤーの動くスピード

    private Vector3 Player_pos; //プレイヤーのポジション
    private float x; //x方向のImputの値
    private float y; //z方向のInputの値
    private Animator anim = null;
    public bool isPlaying;

    void Start()
    {
        Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isPlaying = GManager.instance.isPlaying;
        if (isPlaying)
        {
            x = Input.GetAxisRaw("Horizontal"); //x方向のInputの値を取得
            y = Input.GetAxisRaw("Vertical"); //y方向のInputの値を取得
            if (y == 0 && x == 0)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
            {
                anim.SetBool("run", false);  //  Runモーションしない
            }

            else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
            {
                anim.SetBool("run", true);
            }

            transform.position += new Vector3(x, y, 0).normalized * speed * Time.deltaTime; //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動

            Vector3 diff = transform.position - Player_pos; //プレイヤーがどの方向に進んでいるかがわかるように、初期位置と現在地の座標差分を取得

            if (diff.magnitude > 0.01f) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);  //ベクトルの情報をQuaternion.LookRotationに引き渡し回転量を取得しプレイヤーを回転させる
            }

            Player_pos = transform.position; //プレイヤーの位置を更新

        }
        else
        {
            anim.SetBool("run", false);
        }
    }

}