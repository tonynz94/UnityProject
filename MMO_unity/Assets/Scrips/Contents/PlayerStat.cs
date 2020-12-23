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

    int _itemAttack;
    int _itemDefense;
    int _itemCritical;

    private int _buffAttack = 0;
    private int _buffDefense = 0;
    private int _buffCritical = 0;

    public int BuffAttack { get { return _buffAttack; } set { _buffAttack = value; } }
    public int BuffDefense { get { return _buffDefense; } set { _buffDefense = value; } }
    public int BuffCritical { get { return _buffCritical; } set { _buffCritical = value; } }


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
                Level = level;
                Managers.Skill.LevelUp(Level);
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
        Managers.Skill._level = _level;

        playerUI = GameObject.Find("UI_HPMPEXPBar").GetComponent<UI_PlayerHPBar>();
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[1];
        SetStat(_level);
        _moveSpeed = 15.0f; //임시<<<<<<<<<<<<<<<<<<<
        _exp = 0;
        _gold = 0;
    }

    //레벨업 시 다시 변경되는 스텟.
    public void SetStat()
    {
        SetStat(_level);
    }

    public void SetStat(int level)
    {
        //json의 값들이  Dictionary로 StatDict에 저장되어있음.
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        Data.Stat stat = dict[level];
        _hp = stat.maxHp;
        _mp = stat.maxMp;
        
        _maxHp = stat.maxHp;
        _maxMp = stat.maxMp;

        _attack = stat.attack + Managers.Equip.SumAttack + _buffAttack;
        _defense = stat.defense + Managers.Equip.SumDefense + _buffDefense;
        _critical = stat.critical + Managers.Equip.SumDefense + _buffCritical;
        _evasive = stat.evasive;

        playerUI.LevelUp(level);     
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("BossSkill"))
        {
            OnBossAttack(other.gameObject);
        }
    }

    public void OnBossAttack(GameObject boss)
    {
        BossSkill bossSkill = boss.GetComponent<BossSkill>();
        int damage = Mathf.Max(0, bossSkill.attack - Defense);
        GameObject go = Instantiate(DamageTxt, transform.position, Camera.main.transform.rotation);
        
        go.GetComponent<UI_DmgTxt>().SetDmgText(damage, false, false, Color.blue);

        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
            OnDead(null);
        }
    }

    protected override void OnDead(Stat attacker)
    {
        //자기 자신 삭제.
        Managers.Game.Despawn(gameObject);
    }

    public void EatConsumeItems(int consumeID)
    {
        Hp += Managers.Data.ConsumeItemDict[consumeID].hp;
        Mp += Managers.Data.ConsumeItemDict[consumeID].mp;

        if (Hp > MaxHp)
            Hp = MaxHp;

        if (Mp > MaxMp)
            Mp = MaxMp;
    }

    public GameObject GetPlayerObject()
    {
        return gameObject;
    }
}
