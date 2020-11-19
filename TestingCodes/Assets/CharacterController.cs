using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        Debug.Log(ani.layerCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
