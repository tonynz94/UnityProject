using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Action<int> OnItemChangedCallback = null;

    int space = 20;

    //아이템 창 슬롯
    public List<int> items = new List<int>();

    //아이템을 먹으면 이게 실행되어야 함.
    public bool Add(int itemId)
    {     
        if(itemId != null)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not Enough room.");
                return false;
            }
            Debug.Log("Add Item");
            if (OnItemChangedCallback != null)
            {
                Debug.Log("Invoke Item");
                OnItemChangedCallback.Invoke(itemId);
            }
            Debug.Log(transform.gameObject.name);
            items.Add(itemId);         
        }
        return true;
    }

    public void Remove(int itemId, Sprite icon)
    {
        if (itemId != null)
        {
            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke(itemId);
            items.Remove(itemId);
        }
    }

}