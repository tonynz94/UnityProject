using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAA : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Action Invoke");
        Debug.Log(Managers.Instance);

    }
    private void Update()
    {
        Debug.Log(Managers.Instance);
    }
    public void Say()
    {
        Debug.Log("Hello im AAA Say()");
    }
}
