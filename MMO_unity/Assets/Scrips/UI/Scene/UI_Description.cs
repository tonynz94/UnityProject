using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Description : UI_Base
{
    public Image _itemImage;
    public Text _itemName;
    public Text _itemDescription;
    public enum GameObjects
    {
        ItemImage,
        ItemName,
        ItemDescription
    }
    public override void Init()
    {
        
    }

    private void Awake()
    {
        Bind<GameObject>(typeof(GameObjects));
        _itemImage = Get<GameObject>((int)GameObjects.ItemImage).GetComponent<Image>();
        _itemName = Get<GameObject>((int)GameObjects.ItemName).GetComponent<Text>();
        _itemDescription = Get<GameObject>((int)GameObjects.ItemDescription).GetComponent<Text>();
    }

    public void SetInform(int itemID, Define.InvenType itemType) 
    {
        if (itemType == Define.InvenType.Equipments)
        {
            _itemImage.sprite = Managers.Data.ItemDict[itemID].icon;
            _itemName.text = Managers.Data.ItemDict[itemID].name;
            _itemDescription.text = Managers.Data.ItemDict[itemID].description;
        }
        else if (itemType == Define.InvenType.Consume)
        {
            _itemImage.sprite = Managers.Data.ConsumeItemDict[itemID].icon;
            _itemName.text = Managers.Data.ConsumeItemDict[itemID].name;
            _itemDescription.text = Managers.Data.ConsumeItemDict[itemID].description;
        }
        else if (itemType == Define.InvenType.Others)
        {
            _itemImage.sprite = Managers.Data.OtherItemDict[itemID].icon;
            _itemName.text = Managers.Data.OtherItemDict[itemID].name;
            _itemDescription.text = Managers.Data.OtherItemDict[itemID].description;
        }
    }
}
