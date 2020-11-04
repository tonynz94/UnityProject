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

    // OnUpdate is called in Manager
    public void OnUpdate()
    {
        if(EventSystem.current.IsPointerOverGameObject())   //ui가 클릭 됐을때 
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
                //한번도 누르지 않은 상태일때 실행.
                if(!_pressed)
                {
                    //델리게이트 함수 호출
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;   //마우스를 눌렀을때 실행 됨 시간(초)
                    //Debug.Log(_pressedTime);
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            //누르고 있지 않는 상태라면.
            else
            {
                if (_pressed) //뗏을때 최초 실행.
                {
                    //만약 마우스를 최초로 뗏을때는 실행 해주지 않음 즉 클릭으로 인식하지 않음.
                    if (Time.time < _pressedTime + 0.2f) //누른후 0.2초 내에 땟다면 클릭으로 인정
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
