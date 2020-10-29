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
    //최종적으로 json에 있는 데이터를 저장하는 변수 Stat'
    
    //Dict
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    //Item
    public Dictionary<int, Data.Item> ItemDict { get; private set; } = new Dictionary<int, Data.Item>();


    //매니저에서 호출 해주고 있음.
    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
        ItemDict = LoadJson<Data.ItemData, int, Data.Item>("ItemData").MakeDict();
 
        //StatData.MakeDIct() 함수를 실행해주고 있음.
    }

    //Loader 제네릭은 lLoader라는 클래스나, 인터페이스가 포함되어 있어햐 하는 것. 
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        //파일을 위치를 가져온 후
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        Debug.Log(textAsset.text);
        return JsonUtility.FromJson<Loader>(textAsset.text);    //text로 변환
    }
}
