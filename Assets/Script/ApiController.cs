using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ApiController : MonoBehaviour
{
    string webURL;
    public bool api;
    [SerializeField] private GameObject ItemScrollRect;
    [SerializeField] private GameObject ResultWordList;
    [SerializeField] public Text ItemWord;
    [SerializeField] public GameObject WordButton;
    [SerializeField] public Text CreateWord;
    [SerializeField] public Text CreateMean;

    [SerializeField] public GameObject WordWindow;
    [SerializeField] public Text WindowWord;
    [SerializeField] public Text WindowMean;

    Text buttonText = null;

    private void Start()
    {
        WordWindow.SetActive(false);
        webURL = $"https://enspider.herokuapp.com/items/START";
        StartCoroutine(GetData());

    }

    public void word(string am)
    {
        webURL = $"https://enspider.herokuapp.com/items/{am}";
        //通信はコルーチンを使って行います。
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(webURL);

        yield return request.SendWebRequest();

        //エラー処理
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            //リクエストが成功した時
            if (request.responseCode == 200)
            {
                //サーバーから受信したデータを、UTF8 ストリングとして取得
                string jsonText = request.downloadHandler.text;

                //JSONがunicodeで、仮名がエスケープされてしまうため、エスケープされた文字を変換
                //var JsonText = System.Text.RegularExpressions.Regex.Unescape(jsonText);                

                var items = JsonUtility.FromJson<ItemSchemaArray>(jsonText);

                foreach (var itemSchema in items.items)
                {
                    // 取得した本アイテムをリストに追加
                    api = true;
                    CreateWord.text = itemSchema.word;
                    CreateMean.text = itemSchema.mean.ToString();

                    var wordObj = Instantiate(ItemWord, ItemScrollRect.transform);
                    wordObj.text = itemSchema.word;

                    GameObject resultObj = Instantiate(WordButton);
                    resultObj.transform.SetParent(ResultWordList.transform, false);
                    Button button = resultObj.GetComponent<Button>();
                    buttonText = button.GetComponentInChildren<Text>();
                    buttonText.text = itemSchema.word;
                    button.onClick.AddListener(() => Action(itemSchema.word, itemSchema.mean.ToString()));
                }
            }

        }
    }


    void Action(string word, string mean)
    {
        WordWindow.SetActive(false);
        WordWindow.SetActive(true);
        WindowWord.text = word;
        WindowMean.text = mean;
    }

    [Serializable]
    public class ItemSchema
    {
        public int id;
        public string word;
        public string mean;
    }

    [Serializable]
    public class ItemSchemaArray
    {
        public ItemSchema[] items;
    }
}