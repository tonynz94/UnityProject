using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    //따로 받아서 사용할 일이 없음.
    //상속 받아서 사용할 스크립트이기에 abstract로 함수 설정 -> 클래서 전체가 abstract
    

    public abstract void Init();

    public virtual void Start()
    {
        Init();
    }

    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    //UI를 코드로 가져오는 것.
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;
        return objects[idx] as T;
    }
    
    //T는 컴포넌트를 가리킴
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        //Enum안에 있는 값들의 이름을 가져 옴 
        //type은 enum을 가리킴. enum안에 있는 값들을 string으로 가져옴
        String[] names = Enum.GetNames(type);
        //enum안에 갯수 만큼 배열 생성
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

        //Dictionary에 추가해준다.(T가 가리키는 타입, 해당 게임오브젝드들의 이름들)
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))    //T가 게임오브젝트 타입인 경우.
                objects[i] = Util.FindChild(gameObject, names[i], true);   
            else    //T가 컴포넌트 타입인 경우
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
        }
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx);  }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    // Start is called before the first frame update


    //UI 게임오브젝트에 이벤트를 추가해주는 함수
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        //go라는 오브젝트에 UI_EventHandler 스크립트를 넣어준다 (게임에 들어가기에 생성됨. )
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);
        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:   
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }
}
