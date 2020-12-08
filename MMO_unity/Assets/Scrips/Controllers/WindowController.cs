using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowController : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    static int LastLayerNum = 0;
    public RectTransform _movingWindow;
    public Canvas _canvas;

    public void OnDrag(PointerEventData evt)
    {
        _movingWindow.anchoredPosition += evt.delta / _canvas.scaleFactor;
    }

    public void OnPointerClick(PointerEventData evt)
    {
        _movingWindow.parent.GetComponent<Canvas>().sortingOrder = ++LastLayerNum;
    }
}
