using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _desPos;

    [SerializeField]
    protected GameObject _lockTarget;

    [SerializeField]
    protected Define.State _state = Define.State.Idle;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    public bool _died = false;

    //State 변수는 현재 적용되어 있는 몬스터나 캐릭터가 어떠한 상태인지를 저장해주는 변수
    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();

            switch (_state)
            {
                case Define.State.DIe:
                    anim.CrossFade("DIE", 0.1f, -1, 0);     
                    break;
                case Define.State.Idle:
                    anim.CrossFade("IDLE", 0.1f);
                    break;
                case Define.State.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    break;
                case Define.State.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
                case Define.State.Walk:
                    anim.CrossFade("WALK", 0.1f);
                    break;
                case Define.State.SkillHit:
                    anim.CrossFade("HIT", 0.1f,-1,0);
                    break;
            }
        }
    }
    private void Start()
    {
        //Object.Instantiate(damText, gameObject.transform.position, Camera.main.)
        Init();
    }
   
    protected virtual void Update()
    {
        if (_died)
            return;

        switch (State)
        {
            case Define.State.DIe:
                UpdateDie();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
            case Define.State.Walk:
                UpdateWalk();
                break;
        }
    }
   public abstract void Init();
   protected virtual void UpdateMoving() { }
   protected virtual void UpdateSkill() { }
   protected virtual void UpdateDie() { }
   protected virtual void UpdateIdle() { }
   protected virtual void UpdateWalk() { }
   
}
