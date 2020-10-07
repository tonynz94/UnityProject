using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public PlayerStat _stat;
    bool _moveToDes = false;
    Vector3 _desPos;

    float wait_run_ratio = 0.0f;
   
    [SerializeField]
    PlayerState _state = PlayerState.Idle;
    
    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();

            switch(_state)
            {
                case PlayerState.DIe:
                    anim.SetBool("attack", false);
                    break;
                case PlayerState.Idle:
                    anim.SetFloat("speed", 0);
                    anim.SetBool("attack", false);
                    break;
                case PlayerState.Moving:
                    anim.SetBool("attack", false);
                    anim.SetFloat("speed", _stat.MoveSpeed);
                    break;
                case PlayerState.Skill:
                    anim.SetBool("attack", true);
                    break;
            }
        }
    }


    void Start()
    { 
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

    }

    public enum PlayerState
    {
        DIe,
        Moving,
        Idle,
        Skill,
    };

    void UpdateMoving()
    {
        //타켓이 있을시 몬스터가 내 사정거리보다 가까우면 공격
        if(_lockTarget != null) 
        {
            //자신과 클릭된 적의 거리
            float distance = (_desPos - transform.position).magnitude;
            if(distance <= 1)
            {
                State = PlayerState.Skill;
                return;
            }
        }

        Vector3 dir = _desPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            //TODO
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            //특점 지점으로 갈 수 있는지에 대한 여부
            //nma.CalculatePath
           
            //포지션으로 바로 움직여주는 것이 아닌 navmasAgent로 움직임.
            //정밀도 있게 원하는곳으로 가주지는 않음. 0.0001에서 0.1로 바꿔줘야함.
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.blue);
            if(Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(1) == false)
                    State = PlayerState.Idle;
                return;
            }
            //transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 0.2f);
        }
    }

    void OnRunEvent(int a)
    {
        Debug.Log($"뚜벅뚜벅 {a}");
    }

    void UpdateSkill()
    {

    }

    void Update()
    {
        switch (State)
        {
            case PlayerState.DIe:
                break;
            case PlayerState.Idle:
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Skill:
                UpdateSkill();
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

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    GameObject _lockTarget;


    void OnMouseEvent(Define.MouseEvent evt)
    {
        //if (evt != Define.MouseEvent.Click)
        //   return;

        if (State == PlayerState.DIe)
            return;
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        //LayerMask maskName = 1 << 9;
        //Debug.Log($"dir normalized : {dir}");
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 10, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown: //마우스를 뗀 상태에서 처음으로 딱 누른 상태.
                {
                    if(raycastHit)
                    {
                        //부딪친 좌표
                        _desPos = hit.point;
                        State = PlayerState.Moving;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if(_lockTarget != null)
                        _desPos = _lockTarget.transform.position;
                    else if (raycastHit)
                        _desPos = hit.point;
                }
                break;
        }
    }
}
