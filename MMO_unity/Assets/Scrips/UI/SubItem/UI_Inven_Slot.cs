using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven_Slot : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
    }

    string _name;


    // Start is called before the first frame update

    public override void Init()
    {
        //바인드 했음. 딕셔너리에 넣어줬음.
        base.Bind<GameObject>(typeof(GameObjects));

        //ItemNameText를 가져와서 해당 텍스트를 바꿔 줌

        GameObject Icon = Get<GameObject>((int)GameObjects.ItemIcon);
        BindEvent(Icon, (PointerEventData evt) => { Debug.Log($"아이템 클릭! {_name} , {evt.position}"); });
        //BindEvent(Icon, (PointerEventData evt) => { Transform  }, Define.UIEvent.Drag);
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
