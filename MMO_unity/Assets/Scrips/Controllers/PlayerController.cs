using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    bool _moveToDes = false;
    Vector3 _desPos;

    float wait_run_ratio = 0.0f;
   
    PlayerState _state = PlayerState.Idle;
    void Start()
    {
        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        // TEMP
      
    }

    public enum PlayerState
    {
        DIe,
        Moving,
        Idle
    };

    void UpdateDie()
    {
        //아무것도 못함
    }

    void UpdateIdle()
    {
        //Animation
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed",0);
    
    }

    void UpdateMoving()
    {
        Vector3 dir = _desPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 0.2f);
        }

        //Animation
      
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _speed);
    }

    void OnRunEvent(int a)
    {
        Debug.Log($"뚜벅뚜벅 {a}");
    }
    void Update()
    {
        switch (_state)
        {
            case PlayerState.DIe:
                UpdateDie();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
        }
    }

    void OnKeyBoard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += (Vector3.forward * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (Vector3.back * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += (Vector3.left * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += (Vector3.right * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
        }
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        //if (evt != Define.MouseEvent.Click)
         //   return;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask maskName = 1 << 9;

        //Debug.Log($"dir normalized : {dir}");
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 10, Color.red, 1.0f);
        if (Physics.Raycast(ray, out hit, 100.0f, maskName))
        {
            _desPos = hit.point;
            _state = PlayerState.Moving;
        }
    }
}
