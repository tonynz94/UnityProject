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

    //기타 아이템
    public Dictionary<int, Data.OtherItem> OtherItemDict { get; private set; } = new Dictionary<int, Data.OtherItem>();
    
    //소비 아이템
    public Dictionary<int, Data.ConsumeItem> ConsumeItemDict { get; private set; } = new Dictionary<int, Data.ConsumeItem>();
    //Item
    public Dictionary<int, Data.Item> ItemDict { get; private set; } = new Dictionary<int, Data.Item>();

    //Skill
    public Dictionary<int, Data.Skill> SkillDict { get; private set; } = new Dictionary<int, Data.Skill>();

    //NPC
    public Dictionary<int, Data.NPC> NpcDict { get; private set; } = new Dictionary<int, Data.NPC>();

    //Quest
    public Dictionary<int, Data.Quest> QuestDict {get; private set; } = new Dictionary<int, Data.Quest>();

    public Dictionary<int, Data.Act> ActDict { get; private set; } = new Dictionary<int, Data.Act>();
    //Quest
    //public Dictionary<int,>


    //매니저에서 호출 해주고 있음.
    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
        ItemDict = LoadJson<Data.ItemData, int, Data.Item>("ItemData").MakeDict();
        SkillDict = LoadJson<Data.SkillData, int, Data.Skill>("SkillData").MakeDict();
        OtherItemDict = LoadJson<Data.OtherItemData, int, Data.OtherItem>("OtherItemData").MakeDict();
        ConsumeItemDict = LoadJson<Data.ConsumeItemData, int, Data.ConsumeItem>("ConsumeItemData").MakeDict();
        
        NpcDict = Data.NPCData.MakeDict();
        ActDict = Data.ActData.MakeDict();
        QuestDict = Data.QuestData.MakeDict();

        //StatData.MakeDIct() 함수를 실행해주고 있음.
    }

    //Loader 제네릭은 lLoader라는 클래스나, 인터페이스가 포함되어 있어햐 하는 것. 
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        //파일을 위치를 가져온 후 
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        //json에 있는 값을 변환해줌.
        return JsonUtility.FromJson<Loader>(textAsset.text);    //text로 변환
    }
}
