using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region singleton
    // Start is called before the first frame update

    //클래스 명으로 접근 가능.
    public static UIController instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More then one instance of UI controller found!");
            return;
        }
        instance = this;
    }
    #endregion

    bool _isInventory = false;
    bool _isEquipment = false;

    UI_Inventory _Inven;
    UI_Equipment _Equip;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowEquipment();
        }
    }

    public void ShowInventory()
    {
        _isInventory = !_isInventory;
        if (_isInventory)
        {
            _Inven = Managers.UI.ShowPopupUI<UI_Inventory>("UI_Inven");
        }
        else
        {
            
            if (!_Inven.ClosePopupUI())
                _isInventory = !_isInventory;
        }
    }

    public void ShowEquipment()
    {
        _isEquipment = !_isEquipment;
        if (_isEquipment)
        {
            _Equip = Managers.UI.ShowPopupUI<UI_Equipment>("UI_Equipment");
        }
        else
        {
            if (!_Equip.ClosePopupUI())
                _isEquipment = !_isEquipment;
        }
    }
}
