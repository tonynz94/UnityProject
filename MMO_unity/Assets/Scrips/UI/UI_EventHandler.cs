using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler, IEndDragHandler, IBeginDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    
    //클릭했을때 자동 실행
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        if (OnClickHandler != null && gameObject.GetComponent<Image>().sprite != null)
            OnClickHandler.Invoke(eventData);
    }


    //====드래그 시 자동 실행.====
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Draging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"{eventData.position}");
    }


}
