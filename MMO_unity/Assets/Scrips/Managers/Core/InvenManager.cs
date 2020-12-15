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

    public itemInform[] items = new itemInform[20];
    public int[] _quickSlotConsumeItems = new int[4] {0,0,0,0};


    public void Init()
    {
        for(int i = 0; i < Space; i++)
        {
            items[i] = new itemInform(0, 0, Define.InvenType.None);
        }
    }

    public bool Add(int itemId, Define.InvenType mInvenType)
    {
        Space = 20;

        if(mInvenType != Define.InvenType.Equipments)
        {
            foreach (itemInform item in items)
            {
                if (item.ItemId == itemId)
                {
                    item.count += 1;

                    if (OnItemChangedCallback != null)
                        OnItemChangedCallback.Invoke();

                    return true;
                }
            }    
        }
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
        if (items[itemSlot].invenType == Define.InvenType.Equipments)
        {
            items[itemSlot].Remove();
            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }
        else
        {
            items[itemSlot].count -= 1;
            if(items[itemSlot].count == 0)
            {
                items[itemSlot].Remove();               
            }
            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }
    }

    public void ExchangeSlotItem(int origianlItemSlot, int newItemSlot)
    {
        //새로 클릭한 곳의 아이템이 비어 있을 시
        int tempItemId = items[origianlItemSlot].ItemId;
        int tempItemCount = items[origianlItemSlot].count;
        Define.InvenType tempInvenType = items[origianlItemSlot].invenType;

        items[origianlItemSlot].ItemId = items[newItemSlot].ItemId;
        items[origianlItemSlot].invenType = items[newItemSlot].invenType;
        items[origianlItemSlot].count = items[newItemSlot].count;

        items[newItemSlot].ItemId = tempItemId;
        items[newItemSlot].invenType = tempInvenType;
        items[newItemSlot].count = tempItemCount;

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }

    public void RemoveByItemID(int itemID)
    {
        for(int i = 0; i < Space; i++)
        {
            if(items[i].ItemId == itemID)
            {
                Remove(i);
                return;
            }

        }
    }

    public int FoundSlotByItemID(int itemID)
    {
        for (int i = 0; i < Space; i++)
        {
            if (items[i].ItemId == itemID)
            {
                return i;
            }
        }
        return -1;
    }

}