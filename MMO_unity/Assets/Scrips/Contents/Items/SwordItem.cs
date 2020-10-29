using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : Item
{
    // Start is called before the first frame update
    protected override void Start()
    {
        _itemTemplateId = 10001;
        base.SetItem(_itemTemplateId);
    }
}
