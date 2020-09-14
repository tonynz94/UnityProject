using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnClickHandler != null)
            Debug.Log($"좌표 : ({eventData.position.x} , {eventData.position.y})");
    }

    //드래그 시
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }
}
