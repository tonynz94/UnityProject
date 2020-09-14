using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    GameObject Tank;
    // Start is called before the first frame update
    void Start()
    {
        Tank = Managers.Resource.Instantiate("Tank");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
