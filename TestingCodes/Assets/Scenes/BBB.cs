using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBB : AAA
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Debug.Log("BBB : Start()");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("BBB : Update()");
    }
    protected override void Init()
    {
        Debug.Log("BBB : Init()");
    }
}
