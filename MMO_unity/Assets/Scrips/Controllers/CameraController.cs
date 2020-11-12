using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0,6,-5); //플레이어 기준 카메라 거리

    [SerializeField]
    GameObject _player = null;

    public void SetPlayer(GameObject player) { _player = player; }

    RaycastHit hit;

    // Update is called once per frame
    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuterView)
        {
            //카메라가 앞에 Block이 있는지 확인하는 코드.
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                //상대 게임오브젝트를 지켜보도록 로테이션을 강제로 설정해주는 것.
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
