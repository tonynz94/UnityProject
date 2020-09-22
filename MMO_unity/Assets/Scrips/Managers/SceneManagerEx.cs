﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ex = extended의 약자 
public class SceneManagerEx
{
    public BaseScene CurrentScene{ get { return GameObject.FindObjectOfType<BaseScene>();} }

    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        //reflection으로 Enum이 가지고 있는 값을의 이름을 string으로 가져올수있음.
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }

}
