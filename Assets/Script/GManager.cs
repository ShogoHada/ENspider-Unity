using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    public string alphabet;
    public bool isPlaying = true;
    public List<Vector2> list = new List<Vector2>();
    public float score;



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //void Start()
    //{
    //#if !UNITY_EDITOR && UNITY_WEBGL
    //    WebGLInput.captureAllKeyboardInput = false;
    //#endif
    //}
}
