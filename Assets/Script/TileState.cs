using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileState : MonoBehaviour
{

    public bool NearTile = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider hit)
    //{
    //    if (hit.CompareTag("MyTile"))
    //    {
    //        NearTile = true;
    //    }
    //}

    private void OnTriggerStay(Collider hit)
    {
        if (hit.CompareTag("MyTile"))
        {
            NearTile = true;
        }
    }
}
