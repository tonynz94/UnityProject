using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FindChild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Text co in gameObject.GetComponentsInChildren<Text>())
        {
            Debug.Log(co);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
