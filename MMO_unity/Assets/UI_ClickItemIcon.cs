using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ClickItemIcon : UI_Base
{
    // Start is called before the first frame update

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    Image itemImage;

    public override void Init()
    {

    }

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        BindEvent(gameObject, ItemIconClick);
        BindEvent(gameObject, ItemIconDrag, Define.UIEvent.Drag);
    }
    
    public void ItemIconDrag(PointerEventData evt)
    {
        _rectTransform.anchoredPosition += evt.delta;
    }

    public void ItemIconClick(PointerEventData evt)
    {
        if (evt.button == PointerEventData.InputButton.Left)
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                //if 인벤토리면 
                    //if 해당 칸 아이템이 있는지.
                        //1. 눌렀던 아이템 칸에 누른 칸으로 이동(id)
                        //2. 해당 칸에 놀렀던 아이템 이동(id).
                //else if 장비창안에 있는 이미지를 클릭했다면.
                    //if 해당 칸 아이템이 있는지.
                        //1. 눌렀던 아이템 칸에 누른 칸으로 이동(id)
                        //2. 해당 칸에 놀렀던 아이템 이동(id).
            }
            else
            {
                //땅에 버린다는 것.
            }
        }
    }
}
