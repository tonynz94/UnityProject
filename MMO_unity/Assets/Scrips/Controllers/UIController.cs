using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update

    bool _isInventory = false;
    bool _isEquipment = false;

    UI_Inventory _Inven;

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
            _Inven.ClosePopupUI();
        }
    }

    void ShowEquipment()
    {
       /* if (_isEquipment)
        {
            _Inven = Managers.UI.ShowPopupUI<UI_Inven>("UI_Inven");
        }
        else
        {
            Managers.UI.ClosePopupUI(_Inven);
        }*/
    }
}
