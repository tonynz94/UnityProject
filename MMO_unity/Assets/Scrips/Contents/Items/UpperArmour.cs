using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperArmour : Item
{
    // Start is called before the first frame update
    protected override void Start()
    {
        _itemTemplateId = 3;
        base.SetItem(_itemTemplateId);
    }

}