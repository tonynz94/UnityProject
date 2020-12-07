using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inven_Slot : UI_Base, IDropHandler
{
    public static UI_Notify _notifyUI;
    public int slotPos;

    int itemId;
    GameObject icon;

    enum GameObjects
    {
        ItemIcon,
    }

    // Start is called before the first frame  update
    public void Awake()
    {      
        Bind<GameObject>(typeof(GameObjects));
        icon = Get<GameObject>((int)GameObjects.ItemIcon);

        icon.GetComponent<Image>().enabled = false;
        BindEvent(icon, ItemClick);
    }

    public override void Init()
    {
        Debug.Log("Init enable false");        
    }


    public void AddItem(int itemTemplateId, Define.InvenType type)
    {
        itemId = itemTemplateId;
        if(type == Define.InvenType.Equipments)
            icon.GetComponent<Image>().sprite = Managers.Data.ItemDict[itemTemplateId].icon;
        else if(type == Define.InvenType.Consume)
            icon.GetComponent<Image>().sprite = Managers.Data.ConsumeItemDict[itemTemplateId].icon;
        else if(type == Define.InvenType.Others)
            icon.GetComponent<Image>().sprite = Managers.Data.OtherItemDict[itemTemplateId].icon;

        Debug.Log("enable true");
        icon.GetComponent<Image>().enabled = true;
    }

    public void ClearSlot()
    {
        icon.GetComponent<Image>().sprite = null;
        icon.GetComponent<Image>().enabled = false;     
    }

    public void ItemClick(PointerEventData evt)
    {
        if(evt.button == PointerEventData.InputButton.Left)
        {          
            if (!icon.GetComponent<Image>().IsActive())
                return;
    
            //커서에 클릭한 아이템의 반투명 이미지를 따라 다니게 함.
        }

        //우클릭 시
        else if(evt.button == PointerEventData.InputButton.Right)
        {
            if (!icon.GetComponent<Image>().IsActive())
                return;
            if (Managers.Inven.items[slotPos].invenType != Define.InvenType.Equipments)
                return;

            int tempId = itemId;
            Managers.Inven.Remove(slotPos);
            Managers.Equip.Add(itemId);
            
            
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
    }
}
