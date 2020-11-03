using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Action<int> OnEquipChangedCallback = null;

    //착용한 아이템
    public Dictionary<string ,int> wearItems = new Dictionary<string, int>();
    public int num;
    //아이템을 클릭 시 실행.

    void Start()
    {
        wearItems.Add("hat", 0);
        wearItems.Add("weapon", 0);
        wearItems.Add("shoe", 0);
        wearItems.Add("underArmor", 0);
        wearItems.Add("upperArmor", 0);
    }

    public void Add(Data.Item item)
    {
        //아이템이 없을 시.
        if (wearItems[item.name] == 0)
        {
            wearItems[item.name] = item.itemTemplateId;
        }
        else //해당 칸에 이미 아이템이 있을 시
        {
            //기존에 끼고 있던 것은 다시 장비창으로.
        }

        if(OnEquipChangedCallback != null)
            OnEquipChangedCallback.Invoke(item.itemTemplateId);
    }
}
