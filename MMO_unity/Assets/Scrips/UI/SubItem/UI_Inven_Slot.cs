using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inven_Slot : UI_Base
{
    public static UI_Notify _notifyUI;
    public int slotPos;
    public int itemId = 0;
    public Define.InvenType thisInvenType;

    GameObject icon;
    GameObject itemCount;

    GameObject _itemInform;

    enum GameObjects
    {
        ItemIcon,
        ItemCount,
    }

    // Start is called before the first frame  update
    public void Awake()
    {      
        Bind<GameObject>(typeof(GameObjects));
        icon = Get<GameObject>((int)GameObjects.ItemIcon);
        itemCount = Get<GameObject>((int)GameObjects.ItemCount);

        icon.GetComponent<Image>().enabled = false;
        itemCount.SetActive(false);

        itemId = 0;

        BindEvent(icon, ItemClick);
        BindEvent(icon, MouseEnter, Define.UIEvent.MoushEnter);
        BindEvent(icon, MouseExit, Define.UIEvent.MoushExit);
    }

    public override void Init()
    {
        Debug.Log("Init enable false");        
    }


    public void AddItem(int itemTemplateId, Define.InvenType type, int count = 0)
    {
        itemId = itemTemplateId;
        thisInvenType = type;
        if (type == Define.InvenType.Equipments)
            icon.GetComponent<Image>().sprite = Managers.Data.ItemDict[itemTemplateId].icon;
        else if(type == Define.InvenType.Consume)
            icon.GetComponent<Image>().sprite = Managers.Data.ConsumeItemDict[itemTemplateId].icon;
        else if(type == Define.InvenType.Others)
            icon.GetComponent<Image>().sprite = Managers.Data.OtherItemDict[itemTemplateId].icon;

        //만약 소비창 또는 기타창이면 갯수를 켜 줌.
        if (count != 0)
        {
            itemCount.SetActive(true);
            itemCount.GetComponent<Text>().text = $"{count}";
        }
        else
        {
            itemCount.SetActive(false);
        }


        Debug.Log("enable true");
        icon.GetComponent<Image>().enabled = true;
    }

    public void ClearSlot()
    {
        itemCount.SetActive(false);
        itemCount.GetComponent<Text>().text = "0";
        icon.GetComponent<Image>().sprite = null;
        icon.GetComponent<Image>().enabled = false;
        
        itemId = 0;
        thisInvenType = Define.InvenType.None;
    }

    public void ItemClick(PointerEventData evt)
    {
        if(evt.button == PointerEventData.InputButton.Left)
        {          
            if (!icon.GetComponent<Image>().IsActive())
                return;

            GameObject clickItem = Managers.Resource.Instantiate("UI/Popup/UI_ClickItemIcon", gameObject.transform.parent.parent);
            clickItem.GetComponent<UI_ClickItemIcon>().setIcon(itemId, thisInvenType, slotPos);
            //커서에 클릭한 아이템의 반투명 이미지를 따라 다니게 함.
        }

        //우클릭 시
        else if(evt.button == PointerEventData.InputButton.Right)
        {
            if (!icon.GetComponent<Image>().IsActive())
                return;
            if (Managers.Inven.items[slotPos].invenType == Define.InvenType.Equipments)
            {
                Managers.Sound.Play("Sounds/GameSound/EquipItem");
                Debug.Log(itemId);
                int tempId = itemId;
                Managers.Inven.Remove(slotPos);
                Debug.Log(tempId);
                Managers.Equip.Add(tempId);
            }
            else if(Managers.Inven.items[slotPos].invenType == Define.InvenType.Consume)
            {
                Managers.Sound.Play("Sounds/GameSound/EatingSwallow01");
                Managers.Game.GetPlayer().GetComponent<PlayerStat>().EatConsumeItems(Managers.Inven.items[slotPos].ItemId);
                Managers.Inven.Remove(slotPos);
                
            }
        }
    }

    //마우스가 해당 아이템에 올라갔을때.
    public void MouseEnter(PointerEventData evt)
    {
        if (itemId == 0)
            return;

        if (_itemInform == null)
        {
            GameObject itemDescription = Managers.Resource.Load<GameObject>("Prefabs/UI/Popup/UI_ItemDescription");
            _itemInform = Instantiate(itemDescription, evt.position, itemDescription.transform.rotation, gameObject.transform.parent.parent);
            _itemInform.GetComponent<UI_Description>().SetInform(itemId, thisInvenType);
        }

    }

    public void MouseExit(PointerEventData evt)
    {
        if (_itemInform != null)
        {
            Destroy(_itemInform);
            _itemInform = null;
        }
    }
}
