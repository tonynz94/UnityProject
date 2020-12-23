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
    
    //캐릭터 레벨에 따른 기본 스텟(최대 HP,MP, 해당 레벨이 필요한 겸험치 량) 
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    //기타 아이템
    public Dictionary<int, Data.OtherItem> OtherItemDict { get; private set; } = new Dictionary<int, Data.OtherItem>();
    
    //소비 아이템
    public Dictionary<int, Data.ConsumeItem> ConsumeItemDict { get; private set; } = new Dictionary<int, Data.ConsumeItem>();
    
    //장비 아이템
    public Dictionary<int, Data.Item> ItemDict { get; private set; } = new Dictionary<int, Data.Item>();

    //스킬 포인트에 따른 스킬 공격력, 스킬 쿨 타임 등
    public Dictionary<int, Data.Skill> SkillDict { get; private set; } = new Dictionary<int, Data.Skill>();

    //NPC정보 (대화내용)
    public Dictionary<int, Data.NPC> NpcDict { get; private set; } = new Dictionary<int, Data.NPC>();

    //NPC가 주는 퀘스트정보
    public Dictionary<int, Data.Quest> QuestDict {get; private set; } = new Dictionary<int, Data.Quest>();

    //NPC에게 Yes/No버튼이 있을때 Yes를 누르면 실행할 콜백함수 데이터
    public Dictionary<int, Data.Act> ActDict { get; private set; } = new Dictionary<int, Data.Act>();

    //Managers에서 호출 해주고 있음.
    public void Init()
    {
        //LoadJson으로 클래스를 받아와 Json을 딕셔너리로 변환시켜 
        //Id값을 Key, class를 value로 설정.
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

    //LoadJson으로 Json파일 안에 있는 값을 Text로 가져 온다.
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        //Json의 파일을 로드해 줌. 
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        //json에 있는 값을 클래스의 멤버변수에 이름에 맞춰 값을 초기화 함.
        return JsonUtility.FromJson<Loader>(textAsset.text);   
    }
}
