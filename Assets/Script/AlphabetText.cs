using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class AlphabetText : MonoBehaviour{

    public List<string> boxText = new List<string>();
    public  int boxTextMax = 10;

    [SerializeField] private Text[] texts;
    public static AlphabetText instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetText()
    {
        boxText.Add(GManager.instance.alphabet);
        for (int i = 0; i < boxTextMax; i++)
        {
            if (texts[i].text == "")
            {
                texts[i].text = boxText[i];
                break;
            }
        }
    }


    public void TextDown()
    {
            boxText.RemoveAt(0);
            for (int i = 0; i < boxTextMax; i++)
            {
                if (boxText.Count == i)
                {
                    texts[i].text = "";
                }
            }
            for (int i = 0; i < boxText.Count; i++)
            {
                texts[i].text = boxText[i];
            }
            //Debug.Log(string.Join(",", boxText));
     }

}