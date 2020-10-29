using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Action<int> OnEquipChangedCallback = null;

    //착용한 아이템
    public Dictionary<string ,int> wearItems = new Dictionary<string, int>();

    public bool Add(int itemId)
    {
        
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        wearItems.Add("hat", 0);
        wearItems.Add("weapon", 0);
        wearItems.Add("underArmor", 0);
        wearItems.Add("upperArmor", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
