using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class btnTouch : MonoBehaviour
{
    Vector2 touchPosition = new Vector2(0, 0);
    PointerEventData pointer;
    public ObjectCheck button;
    public bool isPlaying;

    private void Start()
    {
        pointer = new PointerEventData(EventSystem.current);
    }

    void Update()
    {
        isPlaying = GManager.instance.isPlaying;
        if (isPlaying)
        {
            if (Input.touchCount > 0)
            {
                //touchCountが0のときに呼ばれるとエラーでます
                //このフレームでのタッチ情報を取得
                Touch[] myTouches = Input.touches;

                //検出されている指の数だけ回して
                for (int i = 0; i < myTouches.Length; i++)
                {
                    Touch touch = myTouches[i];
                    touchPosition = touch.position;

                    if (Input.GetTouch(i).phase != TouchPhase.Moved && Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        TouchAct(touchPosition);
                    }
                }
            }
        }
    }

    private void TouchAct(Vector2 pos)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        // マウスポインタの位置にレイ飛ばし、ヒットしたものを保存
        pointer.position = pos;
        EventSystem.current.RaycastAll(pointer, results);

        foreach (RaycastResult target in results)
        {

                if (target.gameObject.name == "Zkey")
                {
                    button.Zkey();
                }
                else if (target.gameObject.name == "Xkey")
                {
                    button.Xkey();
                }
                else if (target.gameObject.name == "Ckey")
                {
                    button.Ckey();
                }
        }
        results.Clear();
    }
}
