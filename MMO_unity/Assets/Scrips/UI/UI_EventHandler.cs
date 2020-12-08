using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EventHandler : MonoBehaviour,IPointerDownHandler ,IDragHandler, IPointerClickHandler, IEndDragHandler, IBeginDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    public Action<PointerEventData> OnPointerDownHandler = null;
    public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnEndDragHandler = null;

    //클릭했을때 자동 실행
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    //====드래그 시 자동 실행.====
    //매 프레임마다 실행 됨.
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Draging");
        if (OnDragHandler != null)
        {
            Debug.Log("Action 실행");
            OnDragHandler.Invoke(eventData);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("ClickDown");
    }
}
