using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    //Spawn Position
    public static Vector3 sectionA = new Vector3(130 , 0 , -244);

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
