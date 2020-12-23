using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //현재 카메라의 모드
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuterView;

    //플레이어 기준으로 카메라의 기본 위치
    [SerializeField]
    Vector3 _delta = new Vector3(0,6,-5); 

    [SerializeField]
    GameObject _player = null;

    public void SetPlayer(GameObject player) { _player = player; }

    RaycastHit hit;

    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuterView)
        {
            //메인 캐릭터부터 카메라로 Raycast를 쏴 그 사이에 Block이라는 레이어가 있는지 검사한다.
            //hit에 변수에 부딪친 정보를 가져옴. 
            //True => 중간에 Block이라는 물체가 있다는 것.
            //False => 중간에 방해되는 물체가 없다는 것.
            Debug.Log(_delta.magnitude);
            Debug.DrawRay(_player.transform.position, _delta, Color.red, 1);
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
            {
                //부딪친 오브젝트와 메인플레이어의 거리를 구해 카메라를 80% 거리에 위치시킴
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                //원래 카메라가 지정된 위치에 설정.
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }
    }

    public void SetQuterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuterView;
        this._delta = delta;
    }
}
