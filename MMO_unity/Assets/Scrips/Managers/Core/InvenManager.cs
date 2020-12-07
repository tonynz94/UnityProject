using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenManager
{
    public class itemInform
    {
        public int ItemId;
        public int count;
        public Define.InvenType invenType;
        public itemInform(int mItemId, int mCount, Define.InvenType _invenType)
        {
            ItemId = mItemId;
            count = mCount;
            invenType = _invenType;
        }

        public void Add(int mItemId, int mCount, Define.InvenType _invenType)
        {
            ItemId = mItemId;
            count = mCount;
            invenType = _invenType;
        }

        public void Remove()
        {
            ItemId = 0;
            count = 0;
            invenType = Define.InvenType.None;
        }
    }

    public Action OnItemChangedCallback = null;

    public int _space = 20;
    public int Space { get { return _space; } private set { _space = value; } }

    int countItem = 0;
    public int CountItem { get; private set; }
    //아이템 창 슬롯
    //public int[] items = new int[20] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    public itemInform[] items = new itemInform[20];

    public void Init()
    {
        for(int i = 0; i < Space; i++)
        {
            items[i] = new itemInform(0, 0, Define.InvenType.None);
        }
    }

    public bool Add(int itemId, Define.InvenType mInvenType)
    {
        Debug.Log($"itemId : {itemId}, mInvenType : {mInvenType}");
        Space = 20;
        if (countItem >= Space)
            return false;

        int pos = 0;
        while (items[pos].ItemId != 0)
            ++pos;
            
        items[pos].Add(itemId, 1, mInvenType);
        countItem++;

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    
        return true;
    }

    public void Remove(int itemSlot)
    {
        items[itemSlot].Remove();
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();           
    }

}