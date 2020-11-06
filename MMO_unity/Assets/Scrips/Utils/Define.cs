﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    //enum , const, static 은 인스턴스화 하지 않아도 클래스.xx 로 접근 가능
    namespace SpawnSection
    {
        SectionA = new Vector3(1,2,3),
        SectionB,
        SectionC,
    }


    public enum WorldObject
    {
        Unknown,
        Player,
        Monster
    }


    public enum State
    {
        DIe,
        Moving,
        Idle,
        Skill,
    };

    public enum Layer
    {
        Monster = 8,
        Ground = 9,
        Block = 10,
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
     }


    public enum UIEvent
    {
        Click,
        Drag,
    };

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click
    };

    public enum CameraMode
    {
        QuterView

    };
}
