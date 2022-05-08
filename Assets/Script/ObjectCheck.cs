using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    [SerializeField, Header("アイテムを取得する原点(これを元に近いアイテム、遠いアイテムが決まる)")] private GameObject originPoint;
    //-----タイル-----
    [SerializeField] List<GameObject> nearTile = new List<GameObject>();
    private bool isTileListUpdate;

    [SerializeField] List<GameObject> myTile = new List<GameObject>();
    private bool isMyTileListUpdate;

    [SerializeField] List<GameObject> SetTileList = new List<GameObject>();
    private bool isSetTileListUpdate;

    [SerializeField] List<GameObject> LockTileList = new List<GameObject>();

//-----アルファベット-----
    [SerializeField] GameObject alphabetPrefab = default;
    [SerializeField] List<GameObject> removeAlphabet = new List<GameObject>();
    private bool isAlphabetListUpdate;

    [SerializeField] List<GameObject> NearSetAlpList = new List<GameObject>();
    private bool isNearSetAlpListtUpdate;

    [SerializeField] List<GameObject> SetAlpList = new List<GameObject>();

//-----その他-----
    public List<string> wordList = new List<string>();
    [SerializeField] Material defalut;
    private List<string> UIText;

    public ApiController data;
    public List<string> tempList = new List<string>();
    public Material TileMaterial;

    //-----操作制限-----
    public bool isPlaying;　//ゲーム終了後に操作停止
    private bool keyIsBlock = false; //キー入力ブロックフラグ
    private DateTime pressedKeyTime; //前回キー入力された時間
    private TimeSpan elapsedTime; //キー入力されてからの経過時間
    private TimeSpan CkeyblockTime = new TimeSpan(0, 0, 0, 0, 600); //ブロックする時間
    private TimeSpan blockTime = new TimeSpan(0, 0, 0, 0, 200); //ブロックする時間




    private void Start()
    {
        // アイテム削除関数を実行開始
        StartCoroutine(LateFixedUpdate());
        UIText = AlphabetText.instance.boxText;
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Alphabet"))
        {
            isAlphabetListUpdate = true;
            removeAlphabet.Add(hit.gameObject);
        }
        if (hit.CompareTag("Tile"))
        {
            isTileListUpdate = true;
            nearTile.Add(hit.gameObject);
        }
        if (hit.CompareTag("SetTile"))
        {
            isSetTileListUpdate = true;
            SetTileList.Add(hit.gameObject);
        }
        if (hit.CompareTag("MyTile"))
        {
            isMyTileListUpdate = true;
            myTile.Add(hit.gameObject);
        }
        if (hit.CompareTag("SetAlp"))
        {
            isNearSetAlpListtUpdate = true;
            NearSetAlpList.Add(hit.gameObject);
        }
    }

    private void OnTriggerStay(Collider hit)
    {

        if (hit.CompareTag("Alphabet"))
        {
            isAlphabetListUpdate = true;
            if (removeAlphabet.Contains(hit.gameObject) == false)
            {
                removeAlphabet.Add(hit.gameObject);
            }
        }

        if (hit.CompareTag("Tile") || hit.CompareTag("MyTile"))
        {
            isTileListUpdate = true;
            if (nearTile.Contains(hit.gameObject) == false)
            {
                nearTile.Add(hit.gameObject);
            }
        }
        if (hit.CompareTag("SetTile"))
        {
            isSetTileListUpdate = true;
            if (SetTileList.Contains(hit.gameObject) == false)
            {
                SetTileList.Add(hit.gameObject);
            }
        }
        if (hit.CompareTag("MyTile"))
        {
            isMyTileListUpdate = true;
            if (myTile.Contains(hit.gameObject) == false)
            {
                myTile.Add(hit.gameObject);
            }
        }
        if (hit.CompareTag("SetAlp"))
        {
            isNearSetAlpListtUpdate = true;
            if (NearSetAlpList.Contains(hit.gameObject) == false)
            {
                NearSetAlpList.Add(hit.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("Alphabet"))
        {
            isAlphabetListUpdate = true;
            removeAlphabet.Remove(hit.gameObject);
        }

        if (hit.CompareTag("Tile"))
        {
            isTileListUpdate = true;
            nearTile.Clear();
        }
        if (hit.CompareTag("SetTile"))
        {
            isSetTileListUpdate = true;
            SetTileList.Remove(hit.gameObject);
        }
        if (hit.CompareTag("MyTile"))
        {
            isMyTileListUpdate = true;
            myTile.Remove(hit.gameObject);
        }
        if (hit.CompareTag("SetAlp"))
        {
            isNearSetAlpListtUpdate = true;
            NearSetAlpList.Remove(hit.gameObject);
        }
    }

    /// <summary>
    /// FixedUpdateの実行タイミングで一番最後に実行される関数
    /// </summary>
    /// <returns></returns>
    private IEnumerator LateFixedUpdate()
    {
        var waitForFixed = new WaitForFixedUpdate();
        while (true)
        {
            // 常に更新フラグが立っているか、アイテムリストが更新された時だけ要素の入れ替えを実行
            if (isAlphabetListUpdate)
            {
                PickUpNearItemFirst(removeAlphabet);
                isAlphabetListUpdate = false;
            }
            if (isTileListUpdate)
            {
                PickUpNearItemFirst(nearTile);
                isTileListUpdate = false;
            }
            if (isSetTileListUpdate)
            {
                PickUpNearItemFirst(SetTileList);
                isSetTileListUpdate = false;
            }
            if (isMyTileListUpdate)
            {
                PickUpNearItemFirst(myTile);
                isMyTileListUpdate = false;
            }
            if (isNearSetAlpListtUpdate)
            {
                PickUpNearItemFirst(NearSetAlpList);
                isNearSetAlpListtUpdate = false;
            }
            yield return waitForFixed;
        }
    }
    /// <summary>
    /// 一番近場のアイテムを配列の先頭に持ってくる
    /// </summary>
    /// <returns></returns>
    private void PickUpNearItemFirst(List<GameObject> list)
    {
        if (list.Count <= 1) return;
        var originPos = originPoint.transform.position;
        // 初期最小値を設定
        var minDirection = Vector3.Distance(list[0].transform.position, originPos);
        // 二つ目のアイテムから取得ポイントとの距離を計算
        for (int i = 1; i < list.Count; i++)
        {
            var direction = Vector3.Distance(list[i].transform.position, originPos);
            // より近いオブジェクトを0番目の要素に代入
            if (minDirection > direction)
            {
                minDirection = direction;
                var temp = list[0];
                list[0] = list[i];
                list[i] = temp;
            }
        }
    }

    // アルファベット拾う処理
    private void alpDrow(List<GameObject> list)
    {
        //UI操作
        GManager.instance.alphabet = list[0].GetComponent<SpriteRenderer>().sprite.name;
        AlphabetText.instance.SetText();
        GManager.instance.alphabet = "";
        //アルファベットを消す
        Destroy(list[0].gameObject);
        list.RemoveAt(0);
    }

    // セットされたアルファベット拾う処理
    private void NearAlpDrow()
    {
        if (NearSetAlpList.Count != 0 && SetTileList[0] != null)
        {
            SetTileList[0].GetComponent<Renderer>().material.color = defalut.color;
        }
        //アルファベット拾う
        alpDrow(NearSetAlpList);
        //オブジェクトのタグを変更
        if (SetAlpList.Count - 1 != 0)
        {
            SetAlpList[SetAlpList.Count - 2].tag = "SetAlp";
        }
        if (LockTileList.Count != 0)
        {
            LockTileList[LockTileList.Count - 1].tag = "SetTile";
        }
        if (SetTileList.Count != 0)
        {
            SetTileList[0].tag = "Tile";
        }
        if (LockTileList.Count != 0)
        {
            LockTileList.RemoveAt(LockTileList.Count - 1);
        }
        SetAlpList.RemoveAt(SetAlpList.Count - 1);
        SetTileList.RemoveAt(SetTileList.Count - 1);
        return;
    }
    private void Update()
    {        
        isPlaying = GManager.instance.isPlaying;
        if (isPlaying)
        {
            // アルファベットを拾う操作
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Zkey();
            }

            // アルファベットを落とす操作
            if (Input.GetKeyDown(KeyCode.X))
            {
                Xkey();
            }

            // ワード作成操作
            if (Input.GetKeyDown(KeyCode.C))
            {
                Ckey();
            }
        }
    }

    public void Zkey()
    {
        if (UIText.Count < AlphabetText.instance.boxTextMax)
        {
            if (keyIsBlock)
            {
                elapsedTime = DateTime.Now - pressedKeyTime;
                if (elapsedTime > blockTime)
                {
                    keyIsBlock = false;
                }
                else
                {
                    return;
                }
            }

            keyIsBlock = true;
            pressedKeyTime = DateTime.Now;

            if (NearSetAlpList.Count != 0 && removeAlphabet.Count != 0)
            {
                var originPos = originPoint.transform.position;
                float SetDis = Vector3.Distance(NearSetAlpList[0].transform.position, originPos);
                float RemoveDis = Vector3.Distance(removeAlphabet[0].transform.position, originPos);
                if (SetDis <= RemoveDis)
                {
                    NearAlpDrow();
                }
                else
                {
                    alpDrow(removeAlphabet);

                }
            }
            else if (NearSetAlpList.Count != 0)
            {
                NearAlpDrow();
            }
            else if (removeAlphabet.Count != 0)
            {
                alpDrow(removeAlphabet);
            }
        }
    }

    public void Xkey()
    {
        if (UIText.Count != 0)
        {
            if(keyIsBlock)
                        {
                elapsedTime = DateTime.Now - pressedKeyTime;
                if (elapsedTime > blockTime)
                {
                    keyIsBlock = false;
                }
                else
                {
                    return;
                }
            }

            keyIsBlock = true;
            pressedKeyTime = DateTime.Now;
            if (myTile.Count != 0 && nearTile[0].CompareTag("Tile"))
            {
                TileState script = nearTile[0].GetComponent<TileState>();
                if (script.NearTile)
                {
                    if (SetAlpList.Count == 0 || SetTileList.Count != 0)
                    {
                        //オブジェクトのタグと色を変更
                        if (SetAlpList.Count != 0)
                        {
                            SetAlpList[SetAlpList.Count - 1].tag = "LockAlp";
                        }
                        if (SetTileList.Count != 0)
                        {
                            LockTileList.Add(SetTileList[0]);
                        }
                        if (LockTileList.Count != 0)
                        {
                            SetTileList[0].tag = "LockTile";
                        }
                        string tempTag = "SetTile";
                        nearTile[0].tag = tempTag;
                        nearTile[0].GetComponent<Renderer>().material.color = Color.red;

                        //アルファベットを設置
                        GameObject alphabet = Instantiate(alphabetPrefab, nearTile[0].transform.position, Quaternion.identity);
                        string alphabetSprite = UIText[0];
                        AlphabetText.instance.TextDown();
                        alphabet.GetComponent<SpriteRenderer>().sprite = Load("Sprites", alphabetSprite);
                        var rb = alphabet.GetComponent<Rigidbody>();
                        rb.isKinematic = true;
                        string tempTag2 = "SetAlp";
                        alphabet.tag = tempTag2;
                        SetAlpList.Add(alphabet);

                        NearSetAlpList.Clear();
                        SetTileList.Clear();
                    }
                }
            }
            else
            {
                //アルファベットを捨てる
                GameObject alphabet = Instantiate(alphabetPrefab, originPoint.transform.position, Quaternion.identity);
                string alphabetSprite = UIText[0];
                AlphabetText.instance.TextDown();
                alphabet.GetComponent<SpriteRenderer>().sprite = Load("Sprites", alphabetSprite);
            }
        }
    }

    public void Ckey()
    {
        //連打防止処理
        if (keyIsBlock)
        {
            elapsedTime = DateTime.Now - pressedKeyTime;
            if (elapsedTime > CkeyblockTime)
            {
                keyIsBlock = false;
            }
            else
            {
                return;
            }
        }

        if (myTile.Count != 0)
        {
            if (SetAlpList.Count > 1)
            {
                //連打防止
                keyIsBlock = true;
                pressedKeyTime = DateTime.Now;
                //ワードを確定
                for (int i = 0; i < SetAlpList.Count; i++)
                {
                    tempList.Add(SetAlpList[i].GetComponent<SpriteRenderer>().sprite.name);
                }
                wordList.Add(string.Join("", tempList));
                StartCoroutine("ChangeColor");

            }
        }
    }

    IEnumerator ChangeColor()
    {
        data.word(wordList[0]);
        yield return new WaitForSeconds(0.5f);
        wordList.Clear();

        if (data.api == false)
        {
            tempList.Clear();
            yield break;
        }

        //セットしたタイルを確定
        GameObject temp = GameObject.FindGameObjectWithTag("SetTile");
        temp.tag = "LockTile";
        GameObject[] GO = GameObject.FindGameObjectsWithTag("LockTile");
        foreach (GameObject val in GO)
        {
            val.GetComponent<Renderer>().material.color = TileMaterial.color;
            string tempTag = "MyTile";
            val.tag = tempTag;
        }
        //セットしたアルファベットを確定
        GameObject GOD = GameObject.FindGameObjectWithTag("SetAlp");
        GOD.tag = "MyAlp";
        GameObject[] POD =GameObject.FindGameObjectsWithTag("LockAlp");

        foreach (GameObject val in POD)
        {
            string tempTag = "MyAlp";
            val.tag = tempTag;
        }

        GameObject[] ptemp = GameObject.FindGameObjectsWithTag("MyAlp");
        foreach (GameObject val in ptemp)
        {
            Vector2 pos = val.transform.position;
            if (!GManager.instance.list.Contains(pos))
            {
                GManager.instance.list.Add(pos);
            }
        }

        float MyScore = 0;
        MyScore = 10 * Mathf.Pow(2, tempList.Count-1);
        GManager.instance.score += MyScore;
        tempList.Clear();
        NearSetAlpList.Clear();
        SetAlpList.Clear();
        SetTileList.Clear();
        LockTileList.Clear();
        data.api = false;

    }

    // 落とすアルファベットのスプライト操作
    private Sprite Load(string Sprites, string alphabetSprite)
    {
        // Resoucesから対象のテクスチャから生成したスプライト一覧を取得
        Sprite[] sprites = Resources.LoadAll<Sprite>(Sprites);
        // 対象のスプライトを取得
        return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(alphabetSprite));
    }

}
