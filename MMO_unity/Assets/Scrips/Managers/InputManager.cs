using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager 
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
    //반환형, 매개변수가 없는 델리게이트
    // Start is called before the first frame update

    bool _pressed = false;

    // Update is called once per frame
    public void OnUpdate()
    {
        if(EventSystem.current.IsPointerOverGameObject())   //ui 버튼이 클릭 됐는지.
        {
            return;
        }
        if(Input.anyKey && KeyAction != null)
        {
            KeyAction.Invoke();
        }

        if(MouseAction != null)
        {
            if(Input.GetMouseButton(1)) //누르고 있으면 True
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
