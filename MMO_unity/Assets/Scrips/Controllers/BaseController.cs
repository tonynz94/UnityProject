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

    //혹시 모르기에 virtual로 설정
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
                    //croofade 2번째 인자 => 어느정도 시간이 걸려서 넘어 갈것인지.           
                    break;
                case Define.State.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.State.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    break;
                case Define.State.Skill:
                    //ATTACK에 0.1초의 진입시간, layer는 필요 없기에 -1, 마지막은 다시 하면 0(처음부터) 다시 실행 되는 것.
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
            }
        }
    }
    private void Start()
    {
        //Object.Instantiate(damText, gameObject.transform.position, Camera.main.)
        Init();
    }
   
    void Update()
    {
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
        }
    }
   public abstract void Init();
   protected virtual void UpdateMoving() { }
   protected virtual void UpdateSkill() { }
   protected virtual void UpdateDie() { }
   protected virtual void UpdateIdle() { }
}
