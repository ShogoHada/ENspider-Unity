using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StargeCtrl : MonoBehaviour
{

    [Header("プレイヤーゲームオブジェクト")] public UnityEngine.GameObject playerObj;
    [Header("コンティニュー位置")] public UnityEngine.GameObject continuePoint;

    void Start()
    {
        playerObj.transform.position = continuePoint.transform.position;
    }


    void Update()
    {
        #if UNITY_EDITOR
            //Unityエディタのみで実行したい処理を記述
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GManager.instance.list.Clear();
                GManager.instance.isPlaying = true;
                SceneManager.LoadScene("stage");
            }
        #endif
    }
}
