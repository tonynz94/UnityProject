using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//모든 씬에 대한 최상위 부모
public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
    void Awake()
    {
        //virtual에 자식이 override이기 때문에 자식 Init이 실행 됨
        Init();
    }

    protected virtual void Init()
    {
        //게임 씬 안에 EventSystem을 찾음.
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)    //만약 없다면 생성 후 이름 설정
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear();
}
