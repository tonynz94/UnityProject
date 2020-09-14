using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //현재까지 사용한 팝업
    int _order = 10;

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

    //매니저가 아닌 외부에서 팝업이 생겼을때.
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        //캔버스 안에 캔버스가 있을 시 부모 캔버스를 무시하고 자기만의  order를 가지는 것을 true
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = (_order);
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }


    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;  //T스크립트의 이름

        //오브젝트 생성
        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        //Prefab에 스크립트를 가져오는 것 함수
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;
        go.transform.SetParent(Root.transform);

        return sceneUI;
    }



    //name은 prefab의 이름
    //T는 스크립트 컴포넌트
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;  //T스크립트의 이름

        //오브젝트 생성
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        //Prefab에 스크립트를 가져오는 것 함수
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

            return popup;
    }



    //팝업이 다른 방법으로 닫혔을때.
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        //가장 위에있는게 닫아주고 싶은 팝업이 아니라면.
        if(_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Fail!");
            return;
        }

        ClosePopupUI();
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
}
