using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//오브젝트를 Destory시키면 기존에 해당 오브젝트를 여러게의 변수로 물고 있던 변수들의 주소값은  string "null"로 바뀌게 된다.
//파괴된 오브젝트의 Component에 접근하게 된다면 프로그램에 Crash가 발생하게 된다.
public class GameManager
{
    //서버와 연동한다는 가정.
    //int(id) <-> 짝을 지은 GameObject 상황(이를 dictionary로 관리)
    GameObject _player;
    HashSet<GameObject> _players = new HashSet<GameObject>();
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    public GameObject GetPlayer() { return _player; }
    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }
        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        //부모 스크립트로 접근하여 상속받는 자식이 무슨 스크립트지를 구분
        BaseController bc = go.GetComponent<BaseController>();

        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Monster:
                {
                    if(_monsters.Contains(go))
                        _monsters.Remove(go);
                }
                break;
            case Define.WorldObject.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
        }
        Managers.Resource.Destory(go);
    }
}
