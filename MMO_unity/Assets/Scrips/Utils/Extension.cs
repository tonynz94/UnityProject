﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    //this로 표시된것으로 this.GetOrAddComponent<T>() << 이렇게 호출 가능.
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
   
    public static bool isValid(this GameObject go)
    {
        //setActive인 상태인지 확인
        return go != null && go.activeSelf;
    }

}
