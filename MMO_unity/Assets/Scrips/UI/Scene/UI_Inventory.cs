using UnityEngine;

public class UI_Inventory : UI_Popup
{
    public Transform itemsParent;
    Inventory player;
    UI_Inven_Slot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        player = Managers.Game.GetPlayer().GetComponent<Inventory>();

        player.OnItemChangedCallback -= UpdateUI;
        player.OnItemChangedCallback += UpdateUI;
        itemsParent = gameObject.transform;

        slots = itemsParent.GetComponentsInChildren<UI_Inven_Slot>();

        for (int i = 0; i < slots.Length; i++)
        {
            //플레이어가 가지고 있는 아이템의 수
            if (i < player.items.Count)
            {
                slots[i].AddItem(player.items[i]);
            }
        }
}

    public void UpdateUI(int templateID)
    {
        //20칸
        for(int i = 0; i < slots.Length; i++)
        {
            //플레이어가 가지고 있는 아이템의 수
            if(i <= player.items.Count)
            {
                slots[i].AddItem(templateID);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public override void ClosePopupUI()
    {
        player.OnItemChangedCallback = null;
        base.ClosePopupUI();       
    }

}
