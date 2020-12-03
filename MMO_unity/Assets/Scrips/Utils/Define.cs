using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    //Spawn Position
    public static Vector3 sectionA = new Vector3(-20.91f , 21.7f , 30.39f);

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
        Walk,
        SkillHit,

        SkillQ,
        SkillW,
        SkillE,
        SkillR,

        BossMeleeAttack,
        BossStoneSkill,
        BossJumpSkill,
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
        DragBegin,
        DragEnd,
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

    public enum EquipSlot
    { 
        WeaponSlot,
        HatSlot,
        UpperSlot,
        UnderSlot,
        ShoeSlot,
        MaxSlot,
    };

    public enum SkillName
    { 
        SkillQ,
        SkillW,
        SkillE,
        SkillR
    }

    public enum BossDoor
    {
        Open,
        Close,
    }

    public enum QuestGoalType
    { 
        Kill,
        Gathering,
    }

}
