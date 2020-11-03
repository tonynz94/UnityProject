using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCC : MonoBehaviour
{
    void Start()
    {
       AAA a = GetComponentInParent<AAA>();
       Debug.Log(a.name);
    }
}
