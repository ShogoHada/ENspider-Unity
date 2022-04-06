using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimeScript : MonoBehaviour
{
	public float time = 90;
	public ObjectCheck alphabetScript;
	public GameObject exchangeButton;
	public GameObject result;
	public bool isActive = false;
	public float countdown = 4;
	public GameObject CountDown;
	public Text CountText;

	[SerializeField] public Text TimeText;


	void Start()
	{
		GManager.instance.isPlaying = false;
		isActive = false;
		result.SetActive(false);
		TimeText.text = ((int)time).ToString();
	}

	void Update()
	{

        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            GManager.instance.isPlaying = true;
            isActive = true;
        }
        if (countdown < 0) countdown = 0;
        CountText.text = ((int)countdown).ToString();


        if (!isActive)
		{
			return;
		}
		CountDown.SetActive(false);
		time -= Time.deltaTime;
		if (time < 0)
		{
			StartCoroutine("GameOver");
		}
		if (time < 0) time = 0;
		TimeText.text = ((int)time).ToString();
	}



	IEnumerator GameOver()
	{
		result.SetActive(true);
		exchangeButton.GetComponent<Button>().interactable = true;
		GManager.instance.isPlaying = false;
		yield return new WaitForSeconds(2.0f);
	}

	public void OnButtonClick()
    {
		GManager.instance.list.Clear();
		SceneManager.LoadScene("title");
		exchangeButton.GetComponent<Button>().interactable = false;
	}
}