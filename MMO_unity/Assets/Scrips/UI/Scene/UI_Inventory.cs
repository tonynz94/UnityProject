using UnityEngine;

public class UI_Inventory : UI_Popup
{
    public Transform itemsParent;
    UI_Inven_Slot[] slots;

    // Start is called before the first frame update
    //아이템을 창을 열면 실행되는 부분.
    void Start()
    {
        Managers.Inven.OnItemChangedCallback -= loadInvenUI;
        Managers.Inven.OnItemChangedCallback += loadInvenUI;

        slots = transform.GetComponentsInChildren<UI_Inven_Slot>();
        Debug.Log(Managers.Inven.Space);
        for(int i = 0; i < Managers.Inven.Space; i++)
            slots[i].slotPos = i;
        
        loadInvenUI();
    }

    //기타창이 켜진 상태에서 아이템이 삽입 될때 들어갈때.(invenManager에서 실행이 됨.)
    public void loadInvenUI()
    {
        for (int i = 0; i < Managers.Inven.Space; i++)
        {
            if (Managers.Inven.items[i].ItemId != 0)
                slots[i].AddItem(Managers.Inven.items[i].ItemId, Managers.Inven.items[i].invenType);
            else
                slots[i].ClearSlot();
        }
    }

    public override bool ClosePopupUI()
    {      
        if (base.ClosePopupUI())
        {
            Managers.Inven.OnItemChangedCallback = null;
            return true;
        }
        return false;
    }

    public override string PopUpName()
    {
        return "Inven";
    }
}
