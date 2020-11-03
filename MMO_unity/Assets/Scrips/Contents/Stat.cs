using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    //몬스터 또는 캐릭터가 공통적으로 가지고 있어야 할 정보들

    public GameObject DamageTxt;    

    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _mp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _maxMp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected int _critical;
    [SerializeField]
    protected int _evasive;

    [SerializeField]
    protected float _moveSpeed;


    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int Mp { get { return _mp; } set { _mp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int MaxMp { get { return _maxMp; } set { _maxMp = value; } }

    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public int Critical { get { return _critical; } set { _critical = value; } }
    public int Evasive { get { return _evasive; } set { _evasive = value; } }

    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    protected virtual void Start()
    {
        _level = 1;
        _hp = 100;
        _mp = 100;
        _maxHp = 100;
        _maxMp = 100;

        _attack = 0;
        _defense = 5;
        _critical = 0;
        _evasive = 0;

        _moveSpeed = 5.0f;
        DamageTxt = Managers.Resource.Load<GameObject>("Prefabs/UI/WorldSpace/UI_DamageText");
    }

    //공격을 맞았을 때
    public virtual void OnAttacked(Stat attacker)
    {

        int damage = Mathf.Max(0, attacker.Attack - Defense);
        bool evasive = false;

        //데미지 텍스트의 복사본을 가져옴.
        GameObject go = Object.Instantiate(DamageTxt, transform.position, Camera.main.transform.rotation);


        
        if (Random.value < Evasive*0.01f)
            evasive = true;
        else
            evasive = false;
        go.GetComponent<UI_DmgTxt>().SetDmgText(damage, false ,evasive, Color.blue);

        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    //어떤게 들어올지 모르기 때문에 부모 클래스로 받은 후 캐스팅하여 그게 맞는지 확인

    protected abstract void OnDead(Stat attacker);
}
