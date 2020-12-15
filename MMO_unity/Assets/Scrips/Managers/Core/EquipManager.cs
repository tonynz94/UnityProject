using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
0번 = 무기
1번 = 모자
2번 = 상의
3번 = 하의
4번 = 신발
*/

//캐릭터가 가지고 있는 장비정보
public class EquipManager
{
    public Action OnEquipChangedCallback = null;
    public Action<int> OnWeaponChange = null;
    public Action UpdateStatText = null;
    //착용한 아이템

    public int[] wearItems = new int[5]{ 0,0,0,0,0};
    public int num;

    public GameObject _rightHand;

    public int SumAttack { get; private set; }
    public int SumDefense { get; private set; }
    public int SumCritical { get; private set; }

    //캐릭터가 장착하고 있는 아이템 저장하기
    public void Add(int itemId)
    {
        Debug.Log($"장비 장착할 아이템 아이디 : {itemId}");
        int equipSlot = Managers.Data.ItemDict[itemId].equipSlot;

        //해당 장비창에 이미 다른 아이템을 장착하고 있다면.
        if (wearItems[equipSlot] != 0)
            Managers.Inven.Add(wearItems[equipSlot], Define.InvenType.Equipments);

        wearItems[equipSlot] = itemId;

        //stat창이 열려 있다면.
        if (OnEquipChangedCallback != null)
            OnEquipChangedCallback.Invoke();

        if (equipSlot == 0)
        {
            if (OnWeaponChange != null)
                OnWeaponChange.Invoke(itemId);
        }

        WearingItemsSumStats();
    }

    public void RemoveEquipItem()
    {

    }

    //장착,장착해제 실행.
    public void WearingItemsSumStats()
    {
        SumAttack = 0;
        SumCritical = 0;
        SumDefense = 0;

        for (int i = 0; i < wearItems.Length; i++)
        {
            if (wearItems[i] != 0)
            {
                //고유 ItemId를 가지고 있음.
                SumAttack += Managers.Data.ItemDict[wearItems[i]].attack;
                SumCritical += Managers.Data.ItemDict[wearItems[i]].critical;
                SumDefense += Managers.Data.ItemDict[wearItems[i]].defense;
            }
        }

        GameObject player = Managers.Game.GetPlayer();
        player.GetComponent<PlayerStat>().SetStat();

        if (UpdateStatText != null)
        {
            UpdateStatText.Invoke(); //스탯 텍스트 바꿔 줌
        }
    }
}
