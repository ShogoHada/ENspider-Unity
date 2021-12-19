using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixChildRotation : MonoBehaviour
{
    [SerializeField]
    GameObject Muzzle_E;

    //Quaternion rotationMuzzle;
    //Quaternion rotationMuzzle_E;

    void Awake()
    {
        Muzzle_E.transform.rotation = Quaternion.Euler(0, 0, 30);
    }
    void LateUpdate()
    {
        Muzzle_E.transform.rotation = Quaternion.Euler(0, 0, 30);
    }


}
