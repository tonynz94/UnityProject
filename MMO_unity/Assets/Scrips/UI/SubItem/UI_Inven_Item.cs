using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    string _name;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        //바인드 했음. 딕셔너리에 넣어줬음.
        Bind<GameObject>(typeof(GameObjects));

        //ItemNameText를 가져와서 해당 텍스트를 바꿔 줌
        GameObject  IconText = Get<GameObject>((int)GameObjects.ItemNameText);
        IconText.GetComponent<Text>().text = _name;

        GameObject Icon = Get<GameObject>((int)GameObjects.ItemIcon);
        BindEvent( Icon , (PointerEventData evt) => { Debug.Log($"아이템 클릭! {_name} , {evt.position}"); }     );

    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
