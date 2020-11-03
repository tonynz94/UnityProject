using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update

    bool _isInventory = false;
    bool _isEquipment = false;

    UI_Inventory _Inven;
    UI_Equipment _Equip;

    void Start()
    {
  
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isInventory = !_isInventory;
            ShowInventory();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isEquipment = !_isEquipment;
            ShowEquipment();
        }
    }

    void ShowInventory()
    {   
        if(_isInventory)
        {
            _Inven = Managers.UI.ShowPopupUI<UI_Inventory>("UI_Inven");
        }
        else
        {
            if(!Managers.UI.ClosePopupUI(_Inven))
                _isInventory = !_isInventory;
        }
    }

    void ShowEquipment()
    {
       if (_isEquipment)
        {
            _Equip = Managers.UI.ShowPopupUI<UI_Equipment>("UI_Equipment");
            Debug.Log(_Equip);
        }
        else
        {
            if (!Managers.UI.ClosePopupUI(_Equip))
                _isEquipment = !_isEquipment;
        }
    }
}
