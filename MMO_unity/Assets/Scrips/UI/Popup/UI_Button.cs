using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    public override string PopUpName()
    {
        return null;
    }
    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText      
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetText((int)Texts.ScoreText).text = "Bind Text";

        //ItemIcon이라는 이름을 가진 이미지 오브젝트를 가져온다.
        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        
        //ItemIcon이라는 오브젝트에 이벤트를 추가해준다.(Drag) 이벤트 
        //드래그시 실행하고 싶은 이벤트는 매개변수 :  { go.gameObject.transform.position = data.position; }
        BindEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }
}
