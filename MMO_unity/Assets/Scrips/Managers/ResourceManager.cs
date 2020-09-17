using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    // Start is called before the first frame update


    public T Load<T>(string path) where T : Object
    {
        //게임 오브젝트를 찾는 것
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        //게임 오브젝트 
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if(prefab == null)
        {
            Debug.Log($"Fail to load prefab : {path}");
            return null;
        }

        GameObject go = Object.Instantiate(prefab);
        //이름을 체크 (Clone)해당 문자열을 찾아서 인덱스를 봔환
        int index = go.name.IndexOf("(Clone)");

        //(Clone)을 짤라 줌
        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;
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
