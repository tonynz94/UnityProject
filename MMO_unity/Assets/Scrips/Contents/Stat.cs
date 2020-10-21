using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    //몬스터 또는 캐릭터가 공통적으로 가지고 있어야 할 정보들

    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;


    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    protected virtual void Start()
    {
        _level = 1;
        _hp = 100;
        _attack = 0;
        _maxHp = 100;
        _defense = 5;
        _moveSpeed = 5.0f;

    }

    //공격을 맞았을 때
    public virtual void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defense);
        
        //Instantiate()
        Hp -= damage;
        if(Hp < 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    //어떤게 들어올지 모르기 때문에 부모 클래스로 받은 후 캐스팅하여 그게 맞는지 확인

    protected abstract void OnDead(Stat attacker);
}
