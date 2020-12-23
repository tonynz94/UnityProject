using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager 
{
    //비어 있는 델리게이트를 선언 해줌(PlayerController에서 넣어 주고 있음) 
    //KeyAction -> PlayerController에서 키보드 입력에 따른 코드를 넣어줬음.
    public Action KeyAction = null;

    //KeyAction -> PlayerController에서 마우스 입력에 따른 코드를 넣어줬음.
    public Action<Define.MouseEvent> MouseAction = null;

    // Start is called before the first frame update

    bool _pressed = false;
    float _pressedTime = 0f;

    //해당 OnUpdate문 함수를 Managers에서 Update()함수에서 실행하고 있음.
    public void OnUpdate()
    {
        //키보드 감지에 따른 이벤트 함수  
        if (KeyAction != null)
            KeyAction.Invoke();

        //True -> UI위에 클릭이 됐을때 실행.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //True -> 메인 캐릭터가 말을 하고 있을때 실행.
        if (Managers.Talk._isTalking)
            return;

        //마우스 감지에 따른 이벤트 함수 
        if(MouseAction != null)
        {
            //True -> 우측마우스가 눌러지면 실행.
            if(Input.GetMouseButton(1)) 
            {
                //True -> 첫 클릭일때
                if (!_pressed)
                {    
                    //마우스 델리게이트를 실행시켜줌(PlayerController에 OnMoushEvent실행)
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    //마우스를 최초 클릭시의 시간을 저장 함.
                    _pressedTime = Time.time;   
                }
                //마우스가 Pressed 델리게이트 실행.
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            //누르고 있지 않는 상태라면.
            else
            {
                //True -> 마우스 우측을 클릭 후 최초로 땠을 때 실행 됨
                if (_pressed) 
                {
                    //True -> 마우스 우측을 클릭하고 땠을때의 시간 차가 0.2초 내 일때 실행 됨.
                    if (Time.time < _pressedTime + 0.2f) 
                    {
                        //클릭으로 인식.
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    }
                    //마우스를 그냥 뗀걸로 인식
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
