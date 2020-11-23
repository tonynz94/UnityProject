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


    public void AddItem(int itemTemplateId)
    {
        itemId = itemTemplateId;
        icon.GetComponent<Image>().sprite = Managers.Data.ItemDict[itemTemplateId].icon;
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

            Debug.Log("right click");
            
            //커서에 클릭한 아이템의 반투명 이미지를 따라 다니게 함.
        }

        //우클릭 시
        else if(evt.button == PointerEventData.InputButton.Right)
        {
            if (!icon.GetComponent<Image>().IsActive())
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
