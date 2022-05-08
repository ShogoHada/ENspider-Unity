using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    // 画像を動的に変えたいボタンの宣言
    public Image tutorialImage;

    // inspectorで直接画像のスプライトを張り付ける
    public Sprite Asprite;
    public Sprite Bsprite;
    public Sprite Csprite;
    public Sprite Dsprite;
    public Sprite Esprite;
    public Sprite Fsprite;
    public Sprite Gsprite;
    public Sprite Hsprite;
    public Text btnText;
    public GameObject backButton;

    public Joystick joystick;
    [SerializeField] private GameObject KeyPanel;


    public int i = 1;

    void Start()
    {
        // Imageを所得
        tutorialImage = this.GetComponent<Image>();
        
    }

    void Update()
    {
        if (GManager.instance.PlayMode)
        {
            joystick.gameObject.SetActive(true);
            KeyPanel.gameObject.SetActive(true);
        }
        else
        {
            joystick.gameObject.SetActive(false);
            KeyPanel.gameObject.SetActive(false);
        }
        // フラグによってそれに合った画像に差し替える
        if (i == 1)
        {
            backButton.SetActive(false);

            tutorialImage.sprite = Asprite;
        }
        else if (i == 2)
        {
            backButton.SetActive(true);
            tutorialImage.sprite = Bsprite;
        }
        else if (i == 3)
        {
            tutorialImage.sprite = Csprite;
        }
        else if (i == 4)
        {
            tutorialImage.sprite = Dsprite;
        }
        else if (i == 5)
        {
            tutorialImage.sprite = Esprite;
        }
        else if (i == 6)
        {
            tutorialImage.sprite = Fsprite;
        }
        else if (i == 7)
        {
            tutorialImage.sprite = Gsprite;
            btnText.text = "次へ";
        }
        else if (i == 8)
        {
            tutorialImage.sprite = Hsprite;
            btnText.text = "タイトルへ";
        }
    }

    public void NextButtonClick()
    {
        i++;
        if (i == 9)
        {
            SceneManager.LoadScene("title");
            GManager.instance.PlayMode = false;
        }
    }

    public void BackButtonClick()
    {
        i--;
    }
}