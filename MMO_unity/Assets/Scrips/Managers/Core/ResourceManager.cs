using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        //pool은 최종적인 이름을 사용하고 있음.
        
        //Prefab인지 확인
        //Pooling 인지 확인
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf("/");  //뒤에서 부터 찾음.
            if (index >= 0) //찾았다면.
                name = name.Substring(index + 1); //index + 1부터 끝까지만 name에 저장

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null) //만약 찾아다면.
                return go as T;

        }
        //(원본을 찾아줘라) 창에서 prefab 경로로 가져와서 게임 오브젝트를 가져 옴
        return Resources.Load<T>(path);
    }

    //게임오브젝트를 생성을 해주는 함수.
    public GameObject Instantiate(string path, Transform parent = null)
    {
        //게임 오브젝트의 원본을 가져온다.
        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if(original == null)
        {
            Debug.Log($"Fail to load prefab : {path}");
            return null;
        }

        //Poolable 스크립트가 있다는 것은 오브젝트 풀링을 사용하고 있다는 것.
        if(original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        //풀링이 대상이 아니라면
        //원본을 커피하여 생성해줍니다. 
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;
    }

    //게임오브젝트를 삭제 해주는 함수.
    public void Destory(GameObject go, float time = 0.0f)
    {
        if (go == null)
            return;

        //Poolable 스크립트가 있다는 것은 오브젝트 풀링을 사용하고 있다는 것.
        Poolable poolable = go.GetComponent<Poolable>();

        //만약에 풀어블을 가지고 있다면
        if (poolable != null)
        {
            //오브젝트를 파괴시키지 않고 다시 풀에다가 반환을 하는 것.
            Managers.Pool.Push(poolable);
            return;
        }
        Object.Destroy(go, time);
    }
}
