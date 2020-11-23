using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenManager
{
    public Action OnItemChangedCallback = null;

    public int _space = 20;
    public int Space { get { return _space; } private set { _space = value; } }

    int countItem = 0;
    public int CountItem { get; private set; }
    //아이템 창 슬롯
    public int[] items = new int[20] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

    //아이템을 먹으면 이게 실행됨.
    public bool Add(int itemId)
    {
        Space = 20;
        if (countItem >= Space)
            return false;

        int pos = 0;
        while (items[pos] != 0)
            ++pos;
            
        items[pos] = itemId;
        countItem++;

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    
        return true;
    }

    public void Remove(int itemSlot)
    {
        items[itemSlot] = 0;
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();           
    }

}