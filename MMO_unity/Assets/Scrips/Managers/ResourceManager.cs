using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    // Start is called before the first frame update


    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if(prefab == null)
        {
            Debug.Log($"Fail to load prefab : {path}");
            return null;
        }
        return Object.Instantiate(prefab);
    }

    public void Destory(GameObject go, float time = 0.0f)
    {
        if (go == null)
        {
            return;
        }
        Object.Destroy(go, time);
    }


}
