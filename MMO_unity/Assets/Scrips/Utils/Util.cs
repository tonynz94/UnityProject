using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//오브젝트, 컴포넌트 찾을때 필요한 함수들.
public class Util 
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    { 
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }
    //게임 오브젝트를 찾는 함수
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
        {
            Debug.Log("error");
            return null;
        
        }
        return transform.gameObject;
    }

    //컴포넌트를 찾는 함수
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(recursive == false)
        {
            
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component == null)
                        return component;
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null;
    }
}
