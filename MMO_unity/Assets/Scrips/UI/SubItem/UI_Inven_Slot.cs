using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inven_Slot : UI_Base
{
    GameObject icon;
    GameObject notifyUI;

    Item item;
    enum GameObjects
    {
        ItemIcon,
    }

    string _name;
    // Start is called before the first frame  update
    public void Awake()
    {
        base.Bind<GameObject>(typeof(GameObjects));

        //ItemNameText를 가져와서 해당 텍스트를 바꿔 줌
        notifyUI = Managers.Resource.Load<GameObject>("Prefabs/UI/Popup/UI_Notify");
        icon = Get<GameObject>((int)GameObjects.ItemIcon);
        BindEvent(icon, ItemClick);
        Init();
    }


    public override void Init()
    {
        //바인드 했음. 딕셔너리에 넣어줬음.
        
       
        //BindEvent(Icon, (PointerEventData evt) => { Transform  }, Define.UIEvent.Drag);
    }

    public void SetInfo(string name)
    {
        _name = name;
    }

    public void AddItem(int itemTemplateId)
    {
        icon.GetComponent<Image>().sprite = Managers.Data.ItemDict[itemTemplateId].icon;
        icon.GetComponent<Image>().enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.GetComponent<Image>().sprite = null;
        icon.GetComponent<Image>().enabled = false;
    }

    public void ItemClick(PointerEventData evt)
    {
        //좌클릭시 실행.
        if(evt.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("좌클릭");
            //커서에 클릭한 아이템의 반투명 이미지를 따라 다니게 함.
        }

        //우클릭 시
        else if(evt.button == PointerEventData.InputButton.Right)
        {

            Object.Instantiate<GameObject>(notifyUI);
            //착용하시겠습니까?? 노출
            //확인 -> 장비창에 장착
            //취소 -> 원상태.
        }
    }
}
