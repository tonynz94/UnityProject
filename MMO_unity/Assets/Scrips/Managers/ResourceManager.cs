using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    // Start is called before the first frame update


    public T Load<T>(string path) where T : Object
    {
        //Pool에 저장되어 있다면.
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf("/");
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;

        }


        //(원본을 찾아줘라) 게임 오브젝트를 찾는 것
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        //게임 오브젝트의 원본을 가져옴.
        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if(original == null)
        {
            Debug.Log($"Fail to load prefab : {path}");
            return null;
        }

        //혹시 폴링 되어 있는지.
        if(original.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }


        //원본을 커피해서 go로 만든 것. (과부하)
        //원본을 만들고 parent로 위치 시켜 주라는 뜻.
        GameObject go = Object.Instantiate(original, parent);
        //이름을 체크 (Clone)해당 문자열을 찾아서 인덱스를 봔환
        int index = go.name.IndexOf("(Clone)");

        //(Clone)을 짤라 줌
        if (index > 0)
            go.name = go.name.Substring(0, index);


        //go.name = prefab.name과 같은 것
        return go;
    }

    public void Destory(GameObject go, float time = 0.0f)
    {
        if (go == null)
        {
            return;
        }

        //만약에 풀링이 필요한 오브젝트라면 풀링 매니저로 관리 
        Poolable poolable = go.GetComponent<Poolable>();
        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }


        Object.Destroy(go, time);
    }


}
