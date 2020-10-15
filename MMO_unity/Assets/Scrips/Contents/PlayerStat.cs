using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
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
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
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
            }
        }
    }

    public int Glod { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[1];
        SetStat(_level);
        _defense = 5;
        _moveSpeed = 5.0f;
        _exp = 0;
        _gold = 0;
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[level];
        _hp = stat.maxHp;
        _attack = stat.attack;
        _maxHp = stat.maxHp;
    }

    protected override void OnDead(Stat attacker)
    {
        //자기 자신 삭제.
        Debug.Log("Player Dead");
    }

    public GameObject GetPlayerObject()
    {
        return gameObject;
    }
}
