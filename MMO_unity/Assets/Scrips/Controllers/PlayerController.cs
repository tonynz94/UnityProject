using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    public PlayerStat _stat;
    bool _stopSkill = false;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>();

        //Managers.Input.KeyAction -= OnKeyBoard;
        //Managers.Input.KeyAction += OnKeyBoard;
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    protected override void UpdateMoving()
    {
        //타켓이 있을시 몬스터가 내 사정거리보다 가까우면 공격
        
        if(_lockTarget != null) 
        {
            //자신과 클릭된 적의 거리
            //이게 없으면 공중에 찍어서 1의 값이 넘어가서 가까이 접근을 못하는 상황이 발생
            _desPos = _lockTarget.transform.position;
            float distance = (_desPos - transform.position).magnitude;
            if(distance <= 1)
            {
                State = Define.State.Skill;
                return;
            }
        }

        Vector3 dir = _desPos - transform.position;
        dir.y = 0;

        if (dir.magnitude < 0.1f)
        {
            //이 함수를 실행을 멈춤.
            State = Define.State.Idle;
        }
        else
        {
            //TODO
         
            //특점 지점으로 갈 수 있는지에 대한 여부
            //nma.CalculatePath
           
            //포지션으로 바로 움직여주는 것이 아닌 navmasAgent로 움직임.
            //정밀도 있게 원하는곳으로 가주지는 않음. 0.0001에서 0.1로 바꿔줘야함.
            //충돌처리
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.blue);
            if(Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(1) == false)
                    State = Define.State.Idle;
                return;
            }
            //transform.position += dir.normalized * moveDist;
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 0.2f);
        }
    }

    protected override void UpdateSkill()
    {
        //공격하는 대상 바라보기.
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 20 * Time.deltaTime);
        }
    }

    //애니메이션에서 호출해주고 있음.
    void OnHitEvent()
    {
        if(_lockTarget != null)
        {         
            Stat targetStat = _lockTarget.GetComponent<MonsterStat>();
            targetStat.OnAttacked(_stat);
        }

        if(_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
    }

    //클릭 관련 이벤트가 발생하면 inputManager에서 델리게이트로 실행함.
    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if(evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        if (State == Define.State.DIe)
            return;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 10, Color.red, 1.0f);
        switch (evt)
        {       
            case Define.MouseEvent.PointerDown: //마우스를 뗀 상태에서 처음으로 딱 누른 상태.
                {
                    if (raycastHit)
                    {
                        //부딪친 좌표
                        _desPos = hit.point;
                        State = Define.State.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;              
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _desPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }

    void OnKeyBoard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += (Vector3.forward * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (Vector3.back * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += (Vector3.left * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += (Vector3.right * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
        }
    }
}
