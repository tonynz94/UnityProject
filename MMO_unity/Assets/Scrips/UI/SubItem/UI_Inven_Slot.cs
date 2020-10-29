using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inven_Slot : UI_Base
{
    GameObject icon;
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

        icon = Get<GameObject>((int)GameObjects.ItemIcon);
        Debug.Log($"icon : {icon.name}");
        Debug.Log($"icon.Image : {  icon.GetComponent<Image>()}");
        BindEvent(icon, (PointerEventData evt) => { Debug.Log($"아이템 클릭! {_name} , {evt.position}"); });
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
}
