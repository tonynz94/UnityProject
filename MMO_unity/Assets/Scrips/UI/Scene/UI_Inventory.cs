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
        Debug.Log($"OnItemChangedCallback : {player.OnItemChangedCallback}");
        itemsParent = gameObject.transform;

        slots = itemsParent.GetComponentsInChildren<UI_Inven_Slot>();

        for (int i = 0; i < slots.Length; i++)
        {
            //플레이어가 가지고 있는 아이템의 수
            if (i < player.items.Count)
            {
                Debug.Log(player.items[i]);
                slots[i].AddItem(player.items[i]);
            }
        }
    }

    //10001
    public void UpdateUI(int templateID)
    {
        //20칸
        for(int i = 0; i < slots.Length; i++)
        {
            //플레이어가 가지고 있는 아이템의 수
            //2개면 0 , 1, 2
            if(i < player.items.Count)
            {
                slots[i].AddItem(player.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public override bool ClosePopupUI()
    {
        player.OnItemChangedCallback = null;
        return base.ClosePopupUI();       
    }
}
