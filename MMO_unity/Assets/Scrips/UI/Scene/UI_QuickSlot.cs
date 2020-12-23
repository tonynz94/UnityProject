using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_QuickSlot : UI_Base
{
    UI_QuickSlot_Sub[] _quickSlot;

    public override void Init()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _quickSlot = transform.GetComponentsInChildren<UI_QuickSlot_Sub>();

        for (int i = 0; i < _quickSlot.Length; i++)
        {
            _quickSlot[i]._thisQuickSlotPos = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _quickSlot[0].UsePotion();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _quickSlot[1].UsePotion();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _quickSlot[2].UsePotion();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _quickSlot[3].UsePotion();
        }
    }

    public void ClearQuickSlot(int slotPos)
    {
        _quickSlot[slotPos].ClearQuickSlot();
    }
}
