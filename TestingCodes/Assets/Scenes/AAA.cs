using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AAA : MonoBehaviour
{
    public void Start()
    {
        foreach(int t in CoExample())
        {
            Debug.Log(t);
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynScene(sceneIndex));
    }

    IEnumerator LoadAsynScene(int sceneIndex)
    {
        Debug.Log("Inside IEnumaerator");
            yield return null;
    }


    IEnumerable<int> CoExample()
    {
        yield return 2;
        yield return 3;
        yield return 4;
    }
}
