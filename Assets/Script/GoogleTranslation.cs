//using System;
//using System.Linq;
//using System.Threading;
//using Cysharp.Threading.Tasks;
//using UniRx;
//using UnityEngine;
//using UnityEngine.Networking;
//using UnityEngine.UI;

///// <summary>
///// GoogleTranslationの最小構成サンプル
///// </summary>
//public class GoogleTranslation : MonoBehaviour
//{
//    [SerializeField] private Dropdown fromLanguageDd;
//    [SerializeField] private Dropdown toLanguageDd;
//    [SerializeField] private Button translateButton;
//    [SerializeField] private InputField inputField;
//    [SerializeField] private Text translationText;

//    private const string API_KEY = "AIzaSyDpsVvyTg2jtYlW4O8o47ekodr-zG6Vtd4";
//    private const string ENDPOINT = "https://translation.googleapis.com/language/translate/v2?";

//    /// <summary>
//    /// レスポンスを格納する構造体
//    /// </summary>
//    [Serializable]
//    public struct TranslateData
//    {
//        public Data data;

//        [Serializable]
//        public struct Data
//        {
//            public Translations[] translations;

//            [Serializable]
//            public struct Translations
//            {
//                public string translatedText;
//                public string detectedSourceLanguage;
//            }
//        }
//    }

//    private void Start()
//    {
//        var token = this.GetCancellationTokenOnDestroy();

//        //ドロップダウンメニュー作成
//        var languages = Enum.GetNames(typeof(Language));
//        fromLanguageDd.ClearOptions();
//        fromLanguageDd.AddOptions(languages.ToList());
//        toLanguageDd.ClearOptions();
//        toLanguageDd.AddOptions(languages.ToList());
//        fromLanguageDd.value = (int)fromLanguage;
//        toLanguageDd.value = (int)toLanguage;

//        //翻訳元言語
//        fromLanguageDd.OnValueChangedAsObservable()
//            .Subscribe(value => { fromLanguage = (Language)value; })
//            .AddTo(this);

//        //翻訳後言語
//        toLanguageDd.OnValueChangedAsObservable()
//            .Subscribe(value => { toLanguage = (Language)value; })
//            .AddTo(this);

//        //翻訳ボタン押下
//        translateButton.OnClickAsObservable()
//            .Subscribe(async _ =>
//            {
//                //結果が送られてくるまで待ってから表示
//                var result = GetTranslation(fromLanguage, toLanguage, inputField.text, token);
//                translationText.text = await result;
//            })
//            .AddTo(this);
//    }

//    /// <summary>
//    /// 設定言語
//    /// </summary>
//    private enum Language
//    {
//        ja,
//        en
//    }

//    private Language fromLanguage = Language.en;
//    private Language toLanguage = Language.ja;

//    /// <summary>
//    /// 翻訳結果を返す
//    /// </summary>
//    /// <param name="from">翻訳前の言語設定</param>
//    /// <param name="to">翻訳語の言語設定</param>
//    /// <param name="speechText">翻訳したい文字列</param>
//    /// <param name="ct">CancellationToken</param>
//    /// <returns>翻訳結果</returns>
//    private async UniTask<string> GetTranslation(Language from, Language to, string speechText, CancellationToken ct)
//    {
//        //POSTメソッドのリクエストを作成
//        var requestInfo = ENDPOINT;
//        requestInfo += $"key={API_KEY}&q={speechText}&detectedSourceLanguage={from}&target={to}";
//        var request = UnityWebRequest.Post(requestInfo, "Post");

//        //結果受け取り
//        var second = TimeSpan.FromSeconds(3);
//        var result = await request.SendWebRequest().ToUniTask(cancellationToken: ct).Timeout(second);
//        var json = result.downloadHandler.text;
//        Debug.Log(json);
//        var jsonData = JsonUtility.FromJson<TranslateData>(json);
//        return jsonData.data.translations[0].translatedText;
//    }
//}