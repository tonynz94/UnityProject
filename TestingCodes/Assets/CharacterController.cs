using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public class hello
    {
        public int a = 0;
        public string h = "aa";
    }

    hello[] _hi = new hello[10];
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_hi[5].a);    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
