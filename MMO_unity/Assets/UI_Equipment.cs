using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment : UI_Popup
{
    // Start is called before the first frame update
    GameObject _player;
    
    //List<int> _equipedItems = new List<int>();

    public enum GameObjects
    {
        UI_Weapon_Slot,
        UI_Hat_Slot,
        UI_Upper_Slot,
        UI_Under_Slot,
        UI_Shoe_Slot,
        

        //보여지는 stat창.
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

        Managers.Equip.OnEquipChangedCallback -= UpdateEquipIconUI;
        Managers.Equip.OnEquipChangedCallback += UpdateEquipIconUI;

        Managers.Equip.UpdateStatText -= SetStatText;
        Managers.Equip.UpdateStatText += SetStatText;

        _player.GetComponent<PlayerStat>().UpdateStatText -= SetStatText;
        _player.GetComponent<PlayerStat>().UpdateStatText += SetStatText;

        Bind<GameObject>(typeof(GameObjects));      
    }

    void Start()
    {
        Init();
        UpdateEquipIconUI();
    }

    //랩업, 아이템 장착 해제 실행.
    public void SetStatText()
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

    public void UpdateEquipIconUI()
    {
        for (int i = 0; i < Managers.Equip.wearItems.Length; i++)
        {
            //장착하고 있다는 것.
            int equipItemId = Managers.Equip.wearItems[i];
            if (equipItemId != 0)
            {
                GameObject slot = Get<GameObject>(i);
                slot.GetComponent<Image>().enabled = true;
                slot.GetComponent<Image>().sprite = Managers.Data.ItemDict[equipItemId].icon;
            }
        }
        SetStatText(); //스탯창에 보여지는 값 바꿔주기.
    }

    public override bool ClosePopupUI()
    {
        if(base.ClosePopupUI()){
            _player.GetComponent<PlayerStat>().UpdateStatText = null;
            Managers.Equip.UpdateStatText = null;
            Managers.Equip.OnEquipChangedCallback = null;
            return true;
        }
        return false;
    }

    public void ClickCloseButton()
    {
        UIController.instance.ShowEquipment();
    }
}
