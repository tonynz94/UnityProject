using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAA : MonoBehaviour
{
    // Start is called before the first frame update
    protected void Start()
    {
        Debug.Log("AAA : Start()");
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Init()
    {
        Debug.Log("AAA : Init()");
    }
}
