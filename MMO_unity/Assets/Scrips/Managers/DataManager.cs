using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이를 상속 받는 클래스는 반드시 구현해야 한다는 것.
public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();  
}

public class DataManager
{
    public Dictionary<int, Stat> StatDict { get; private set; } = new Dictionary<int, Stat>();
    
    public void Init()
    {
        StatDict = LoadJson<StatData, int, Stat>("StatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        //파일을 위치를 가져온 후
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);    //text팡ㄹ로 변환
    }
}
