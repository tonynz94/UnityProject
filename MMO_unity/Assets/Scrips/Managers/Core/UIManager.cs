using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//팝업 관리(맨마지막에 켜진 팝업을 가장 먼저 꺼지도록 관리해주는 클래스)
public class UIManager
{
    //0이면 sort를 하지 않는 UI와 같은 우선순위이기에 10부터 시작.
    int _order = 10;

    //UI_Popup을 상속받고 있음.
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;


    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    //매니저가 아닌 외부에서 팝업이 생겼을때 order에 채워주는 함수.
    //UI_PopUp 스크립트에서 바로 실행 해 줌
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        Debug.Log($"canvas : {canvas}");
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        //캔버스 안에 캔버스가 있을 시 부모 캔버스를 무시하고 자기만의  order를 가지는 것을 true
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = (_order);
            _order++;
        }
        else //씬 UI인 경우 
        {
            canvas.sortingOrder = 0;
        }
    }

    //3D 월드 UI (몬스터 HP)
    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {

        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;  //T스크립트의 이름

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return go.GetOrAddComponent<T>();
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;  //T스크립트의 이름

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return go.GetOrAddComponent<T>();
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;  //T스크립트의 이름

        //게임오브젝트 게임씬에 생성
        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        //Prefab에 스크립트를 가져오고(UI_Button)
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;
        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    //name : Prefab의 이름
    //<T> : 스크립트
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        //script와 Prefab 이름이 대부분 같음
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;  //T스크립트의 이름

        //게임오브젝트 씬에 등장.
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        //Prefab에 스크립트를 가져오는 것 함수
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    //팝업이 다른 방법으로 닫혔을때.
    public bool ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return false;

        //가장 위에있는게 닫아주고 싶은 팝업이 아니라면.
        if(_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Fail!");
            return false;
        }

        ClosePopupUI();
        return true;
    }

    public void ClosePopupUI()
    {
        //팝업이 하나도 노출되어 있지 않으면 실행
        if (_popupStack.Count == 0)
            return;


        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destory(popup.gameObject);
        popup = null;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }


}
