﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatItem : Item
{
    // Start is called before the first frame update
    protected override void Start()
    {
        _itemTemplateId = 10002;
        base.SetItem(_itemTemplateId);
    }

}