﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderArmour : Item
{
    // Start is called before the first frame update
    protected override void Start()
    {
        _itemTemplateId = 4;
        base.SetItem(_itemTemplateId);
    }

}
