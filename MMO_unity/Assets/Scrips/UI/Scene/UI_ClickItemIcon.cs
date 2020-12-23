using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ClickItemIcon : UI_Base
{
    // Start is called before the first frame update

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Image _itemImage;

    int clickItemID;
    int clickSlotPos;
    Define.InvenType clickInvenType;

    private void Update()
    {
        gameObject.transform.position = Input.mousePosition;
    }

    public override void Init()
    {

    }

    void Awake()
    {
        _itemImage = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        BindEvent(gameObject, ItemIconClick,Define.UIEvent.ClickDown);
    }
    
    public void setIcon(int itemId, Define.InvenType invenType, int slotPos)
    {
        _itemImage.color = new Color(1, 1, 1, 0.75f);

        clickItemID = itemId;
        clickSlotPos = slotPos;
        clickInvenType = invenType;

        if (invenType == Define.InvenType.Equipments)
            _itemImage.sprite = Managers.Data.ItemDict[itemId].icon;
        else if (invenType == Define.InvenType.Consume)
            _itemImage.sprite = Managers.Data.ConsumeItemDict[itemId].icon;
        else if (invenType == Define.InvenType.Others)
            _itemImage.sprite = Managers.Data.OtherItemDict[itemId].icon;

        
    }

    public void ItemIconClick(PointerEventData evt)
    {
        if (evt.button == PointerEventData.InputButton.Left)
        {
            List<RaycastResult> _raycastResultList = new List<RaycastResult>();
            if (EventSystem.current.IsPointerOverGameObject())
            {
                EventSystem.current.RaycastAll(evt, _raycastResultList);
                if(_raycastResultList.Count > 0)
                {
                    foreach(RaycastResult raycastReult in _raycastResultList)
                    {
                        if(raycastReult.gameObject.name == "UI_Inven_Slot")
                        {
                            UI_Inven_Slot uI_Inven_Slot = raycastReult.gameObject.GetComponent<UI_Inven_Slot>();

                            Managers.Inven.ExchangeSlotItem(clickSlotPos , uI_Inven_Slot.slotPos);
                            break;
                        }

                        else if(raycastReult.gameObject.name == "QuickSlot")
                        {
                            UI_QuickSlot_Sub uI_QuickSlot_Sub = raycastReult.gameObject.GetComponent<UI_QuickSlot_Sub>();
                            uI_QuickSlot_Sub.DoRegistPotion(clickSlotPos);
                        }   
                    }
                }
                Destroy(gameObject);
            }
            else
            {
                //땅에 버린다는 것.
            }
        }
    }
}
