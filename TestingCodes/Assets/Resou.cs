using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resou : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       GameObject cube = Resources.Load<GameObject>("Cube");
       GameObject copy = Object.Instantiate<GameObject>(cube);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
