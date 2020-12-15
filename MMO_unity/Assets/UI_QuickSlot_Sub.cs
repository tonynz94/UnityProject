using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuickSlot_Sub : UI_Base
{
    public GameObject _quickSlotsObject;


    UI_QuickSlot _quickSlotsScript;
    GameObject _itemImage;
    GameObject _itemCount;
    
    public int _thisQuickSlotPos;

    int _consumeID;
    // Start is called before the first frame update
    public enum GameObjects
    {
        ItemImage,
        ItemCount,
    }

    void Awake()
    {
        Bind<GameObject>(typeof(GameObjects));

        _itemCount = Get<GameObject>((int)GameObjects.ItemCount);
        _itemImage = Get<GameObject>((int)GameObjects.ItemImage);
        _quickSlotsScript = _quickSlotsObject.GetComponent<UI_QuickSlot>();

        _consumeID = 0;
    }

    void Start()
    {
        ClearQuickSlot();
    }

    public override void Init()
    {

    }

    //포션 등록
    public void DoRegistPotion(int clickItemPos)
    {
        if (Managers.Inven.items[clickItemPos].invenType != Define.InvenType.Consume)
        {
            Debug.Log("소비창만 등록할 수 있음");
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            //이미 등록 되어 있는 포션이라면
            if (Managers.Inven._quickSlotConsumeItems[i] == Managers.Inven.items[clickItemPos].ItemId) {
                //해당 칸의 아이디를 0으로 바꿔주고.
                Managers.Inven._quickSlotConsumeItems[i] = 0;
                _quickSlotsScript.ClearQuickSlot(i);
                break;
            }
        }

        if (Managers.Inven._quickSlotConsumeItems[_thisQuickSlotPos] != 0)
        {
            Debug.Log("기존에 있던거 삭제");
            ClearQuickSlot();
        }
        _consumeID = Managers.Inven.items[clickItemPos].ItemId;
        Managers.Inven._quickSlotConsumeItems[_thisQuickSlotPos] = _consumeID;
        _itemCount.SetActive(true);

        _itemImage.GetComponent<Image>().sprite = Managers.Data.ConsumeItemDict[_consumeID].icon;
        _itemCount.GetComponent<Text>().text = $"{Managers.Inven.items[clickItemPos].count}";
    }

    //퀵슬롯 삭제.
    public void ClearQuickSlot()
    {
        _itemImage.GetComponent<Image>().sprite = null;
        _itemCount.GetComponent<Text>().text = "";
        _itemCount.SetActive(false);
        _consumeID = 0;
    }

    public void UsePotion()
    {
        //아무것도 등록되지 않음
        if (_consumeID == 0)
        {
            return;
        }
      
        Managers.Sound.Play("Sounds/GameSound/EatingSwallow01");
        Managers.Game.GetPlayer().GetComponent<PlayerStat>().EatConsumeItems(_consumeID);
        Managers.Inven.RemoveByItemID(_consumeID);
        UpdateQuickSlot();
    }

    public void UpdateQuickSlot()
    {
        int consumeItemPos = Managers.Inven.FoundSlotByItemID(_consumeID);

        if (consumeItemPos != -1)
        {
            _itemImage.GetComponent<Image>().sprite = Managers.Data.ConsumeItemDict[_consumeID].icon;
            _itemCount.GetComponent<Text>().text = $"{Managers.Inven.items[consumeItemPos].count}";
        }
        else
        {
            ClearQuickSlot();
        }
    }

    
}
