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
    float _pressedTime = 0f;

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
                if(!_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    if (Time.time < _pressedTime * 0.2f) //누른후 0.2초 내에 땟다면 클릭으로 인정
                    {
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    }
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);

                }
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
