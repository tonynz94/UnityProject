using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment : UI_Popup
{
    // Start is called before the first frame update
    GameObject _player;

    //List<int> _equipedItems = new List<int>();

    enum GameObjects
    {
        UI_Upper_Slot,
        UI_Hat_Slot,
        UI_Under_Slot,
        UI_Shoe_Slot,
        UI_Weapon_Slot,
        LevelText,
        HPText,
        MPText,
        AttackText,
        DefenseText,
        CriticalText,
        EvasiveText
    }


    //게임씬에 생성 됐을 때.
    private void Awake()
    {
        _player = Managers.Game.GetPlayer();

        _player.GetComponent<Equipment>().OnEquipChangedCallback -= UpdateEquipIconUI;
        _player.GetComponent<Equipment>().OnEquipChangedCallback += UpdateEquipIconUI;

        Bind<GameObject>(typeof(GameObjects));      
    }

    void Start()
    {
        Init();
        if (_player.GetComponent<Equipment>().wearItems["hat"] != 0)
            UpdateEquipIconUI(_player.GetComponent<Equipment>().wearItems["hat"]);

        if (_player.GetComponent<Equipment>().wearItems["weapon"] != 0)
            UpdateEquipIconUI(_player.GetComponent<Equipment>().wearItems["weapon"]);

        if (_player.GetComponent<Equipment>().wearItems["shoe"] != 0)
            UpdateEquipIconUI(_player.GetComponent<Equipment>().wearItems["shoe"]);

        if (_player.GetComponent<Equipment>().wearItems["upperArmor"] != 0)
            UpdateEquipIconUI(_player.GetComponent<Equipment>().wearItems["upperArmor"]);

        if (_player.GetComponent<Equipment>().wearItems["underArmor"] != 0)
            UpdateEquipIconUI(_player.GetComponent<Equipment>().wearItems["underArmor"]);

        SetStatText();
    }

    public void SetStatText(int updataStat = 0)
    {
        PlayerStat stat = _player.GetComponent<PlayerStat>();
        Get<GameObject>((int)GameObjects.LevelText).GetComponent<Text>().text = $"{stat.Level}";
        Get<GameObject>((int)GameObjects.HPText).GetComponent<Text>().text = $"{stat.MaxHp}";
        Get<GameObject>((int)GameObjects.MPText).GetComponent<Text>().text = $"{stat.MaxMp}";
        Get<GameObject>((int)GameObjects.AttackText).GetComponent<Text>().text = $"{stat.Attack}";
        Get<GameObject>((int)GameObjects.DefenseText).GetComponent<Text>().text = $"{stat.Defense}";
        Get<GameObject>((int)GameObjects.CriticalText).GetComponent<Text>().text = $"{stat.Critical}";
        Get<GameObject>((int)GameObjects.EvasiveText).GetComponent<Text>().text = $"{stat.Evasive}";
    }

    public void UpdateEquipIconUI(int tempID)
    {
        GameObject equipSlot = null;
 
        if (Managers.Data.ItemDict[tempID].name == "hat")
            equipSlot = Get<GameObject>((int)GameObjects.UI_Hat_Slot);
        else if (Managers.Data.ItemDict[tempID].name == "weapon")
            equipSlot = Get<GameObject>((int)GameObjects.UI_Weapon_Slot);
        else if (Managers.Data.ItemDict[tempID].name == "shoe")
            equipSlot = Get<GameObject>((int)GameObjects.UI_Shoe_Slot);
        else if (Managers.Data.ItemDict[tempID].name == "upperArmor")
            equipSlot = Get<GameObject>((int)GameObjects.UI_Upper_Slot);
        else if (Managers.Data.ItemDict[tempID].name == "underArmor")
            equipSlot = Get<GameObject>((int)GameObjects.UI_Under_Slot);

        if(equipSlot == null)
        {
            Debug.Log($"equipSlot is {equipSlot}");
            return;
        }
        equipSlot.GetComponent<Image>().enabled = true;

        //아이콘을 바꿔주는 동시에 스탯도 바꿔주기.
        equipSlot.GetComponent<Image>().sprite = Managers.Data.ItemDict[tempID].icon;


    }

    public void ClickCloseButton()
    {
        UIController.instance.ShowEquipment();
    }
}
