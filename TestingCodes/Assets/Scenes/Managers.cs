using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // Start is called before the first frame update
    public static Managers Instance;

    public Action test = null;

    void Start()
    {    
        if(Instance != null)
        {
            Debug.Log("More the one Managers");
        }
        Instance = this;
    }

    private void Update()
    {

    }

    // Update is called once per frame

}
