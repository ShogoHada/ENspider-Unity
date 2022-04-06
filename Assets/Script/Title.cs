using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        GManager.instance.isPlaying = true;
    //        SceneManager.LoadScene("stage");
    //    }
    //}

    public void StartButtonClick()
    {
        GManager.instance.isPlaying = true;
        SceneManager.LoadScene("stage");
    }

    public void TutorialButtonClick()
    {
        SceneManager.LoadScene("tutorial");
    }
}
