using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    //enum , const, static 은 인스턴스화 하지 않아도 클래스.xx 로 접근 가능

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game
    }


    public enum UIEvent
    {
        Click,
        Drag,
    };

    public enum MouseEvent
    {
        Press,
        Click
    };

    public enum CameraMode
    {
        QuterView

    };
}
