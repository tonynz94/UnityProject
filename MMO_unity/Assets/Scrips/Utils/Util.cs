using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//오브젝트, 컴포넌트 찾을때 필요한 함수들.
//공통적으로 사용 할 수 있는 기능들을 모아 놓는곳
public class Util 
{
    //go 대상의 오브젝트 안에 컴포넌트를 추가 또는 생성 해줄때 사용되는 함수입니다.
    //컴포넌트가 존재한다면 해당 컴포넌트를 반환해주며.
    //만약 없다면 추가 해줍니다.
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    { 
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }


    //===============자식오브젝트에서의 컴포너틑를 찾는 함수=================
    //go 오브젝트의 자식 오브젝트를 찾는 함수입니다.
    //매개변수로는 (부모 오브젝트, 찾고 싶은 오브젝트 이름, 재귀여부)
    //재귀여부는 자식안에 자식까지 찾는지에 대한 여부 입니다.
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
        {
            return null;  
        }
        return transform.gameObject;
    }

    //go 오브젝트의 자식 오브젝트의 컴포넌트를 찾는 함수입니다.
    //제네릭 <T> 찾고 싶은 컴포넌트를 의미합니다.
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        //recursive == false라는 건 자식의 자식까지 찾지 않겠다라는 뜻.
        //(한단계의 자식까지만 찾겠다라는 것.)
        if (recursive == false)
        {
            
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            //recursive == True라는 건 자식의 자식까지 찾겠다라는 뜻.
            //(한단계를 넘어 2단계 3단계...n단계의 자식까지 찾겠다라는 것.)
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null;
    }
}
