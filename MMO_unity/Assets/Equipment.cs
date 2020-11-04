using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//캐릭터가 가지고 있는 장비정보
public class Equipment : MonoBehaviour
{
    public Action<int> OnEquipChangedCallback = null;
    public Action UpdateStatText = null;
    //착용한 아이템
    public Dictionary<string ,int> wearItems = new Dictionary<string, int>();
    public int num;

    //아이템을 클릭 시 실행.

    int sumAttack;
    int sumDefense;
    int sumCritical;

    public int SumAttack { get; private set; }
    public int SumDefense { get; private set; }
    public int SumCritical { get; private set; }

    void Awake()
    {
        sumAttack = 0;
        sumDefense = 0;
        sumCritical = 0;
    }

    void Start()
    {
        wearItems.Add("hat", 0);
        wearItems.Add("weapon", 0);
        wearItems.Add("shoe", 0);
        wearItems.Add("underArmor", 0);
        wearItems.Add("upperArmor", 0);
    }

    //캐릭터가 장착하고 있는 아이템 저장하기
    public void Add(Data.Item item)
    {
        //아이템이 없을 시.
        if (wearItems[item.name] == 0)
        {
            Debug.Log($"장착 완료 : {item.name}");
            wearItems[item.name] = item.itemTemplateId;
        }
        else //해당 칸에 이미 아이템이 있을 시
        {
            //기존에 끼고 있던 것은 다시 장비창으로.
        }

        //stat창이 열려 있다면.
        if (OnEquipChangedCallback != null)
        {
            //UI_Equipment에 접근해서 icon을 바꿔주는것
            OnEquipChangedCallback.Invoke(item.itemTemplateId); 
        }
        WearingItemsSumStats();
    }

    //장착,장착해제,레벨업 때 실행.
    public void WearingItemsSumStats()
    {
        SumAttack = 0;
        SumCritical = 0;
        SumDefense = 0;

        for (int i = 0; i < wearItems.Count; i++)
        {
            //착용하지 않았다는 것.
            //10001 : 
            int wearingItemID = wearItems.Values.ToList<int>()[i];
            if (wearingItemID != 0)
            {
                //고유 ItemId를 가지고 있음.
                SumAttack += Managers.Data.ItemDict[wearingItemID].attack;
                SumCritical += Managers.Data.ItemDict[wearingItemID].critical;
                SumDefense += Managers.Data.ItemDict[wearingItemID].defense;
            }
        }
        //만약 스탯창이 켜지 있지않ㅇ면 실행X
        gameObject.GetComponent<PlayerStat>().SetStat(gameObject.GetComponent<PlayerStat>().Level);
        if (UpdateStatText != null)
        {
            UpdateStatText.Invoke(); //스탯 텍스트 바꿔 줌
        }
    }
}
