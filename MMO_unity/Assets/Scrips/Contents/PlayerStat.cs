using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : Stat
{
    public UI_PlayerHPBar playerUI;
    public GameObject _player;
    public Action UpdateStatText = null;

    [SerializeField]
    int _exp;
    [SerializeField]
    int _gold;

    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;

            int level = Level;
            while(true)
            {
                Data.Stat stat;
                //딕셔너리에서 키 값으로 가져오는 것.
                //다음 레벨의 스텟을 가져옴. 
                if (Managers.Data.StatDict.TryGetValue(level, out stat) == false)
                    break;
                if (_exp < stat.totalExp)
                    break;
                level++;
            }

            if(level != Level)
            {
                Debug.Log("Level Up");
                Level = level;
                SetStat(Level);
                if (UpdateStatText != null)
                {
                    UpdateStatText.Invoke(); //스탯 텍스트 바꿔 줌
                }
            }
        }
    }

    public int Glod { get { return _gold; } set { _gold = value; } }

    protected override void Start()
    {
        base.Start();
        _player = Managers.Game.GetPlayer();
        _level = 1;

        playerUI = GameObject.Find("UI_HPMPEXPBar").GetComponent<UI_PlayerHPBar>();
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[1];
        SetStat(_level);
        _moveSpeed = 5.0f;
        _exp = 0;
        _gold = 0;
    }

    //레벨업 시 다시 변경되는 스텟.
    public void SetStat(int level)
    {
        //json의 값들이  Dictionary로 StatDict에 저장되어있음.
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Equipment equipItems = _player.GetComponent<Equipment>();

        Data.Stat stat = dict[level];
        _hp = stat.maxHp;
        _mp = stat.maxMp;
        
        _maxHp = stat.maxHp;
        _maxMp = stat.maxMp;

        _attack = stat.attack + equipItems.SumAttack;
        _defense = stat.defense + equipItems.SumDefense;
        _critical = stat.critical + equipItems.SumCritical; 
        _evasive = stat.evasive;

        playerUI.LevelUp(level);

        
    }

    public void sumItemsStatAndCharacterStat()
    {
        

    }

    protected override void OnDead(Stat attacker)
    {
        //자기 자신 삭제.
        Managers.Game.Despawn(gameObject);
    }

    public GameObject GetPlayerObject()
    {
        return gameObject;
    }
}
