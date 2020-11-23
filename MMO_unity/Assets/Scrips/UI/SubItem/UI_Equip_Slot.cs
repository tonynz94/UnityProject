using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//각 Slot 에 들어갈 예정.
public class UI_Equip_Slot : UI_Base
{
    GameObject _equipButton;
    GameObject _player;

    public UI_Equipment.GameObjects slotName;

    enum GameObjects
    {
        EquipSlotButton,
    }

    public override void Init()
    {

    }

    public void Awake()
    {
        _player = Managers.Game.GetPlayer();
        base.Bind<GameObject>(typeof(GameObjects));
        _equipButton = Get<GameObject>((int)GameObjects.EquipSlotButton);
        BindEvent(_equipButton, ItemClick);

    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    //장비 해제
    public void ItemClick(PointerEventData evt)
    {
        
        UI_Equipment UIEquip = gameObject.transform.GetComponentInParent<UI_Equipment>();
        int removeItemID = Managers.Equip.wearItems[(int)slotName];
        if (removeItemID == 0) {
            Debug.Log("No item clicked");
            return;
        }
        
        //인벤토리에 추가
        if(!Managers.Inven.Add(removeItemID))
        {
            Debug.Log("No space to UnAttach");
            return;
        }

        //장비창의 정보를 삭제 시켜주고 아이템 스탯도 다시 계산해 줌.
        Managers.Equip.wearItems[(int)slotName] = 0;
        Managers.Equip.WearingItemsSumStats();


        //아이콘 삭제.
        Transform equipItemIcon = gameObject.transform.GetChild(0);
        if (equipItemIcon.GetComponent<Image>().sprite == null) {
            return;
        }
        equipItemIcon.GetComponent<Image>().sprite = null;
        equipItemIcon.GetComponent<Image>().enabled = false;
    } 

    //장비창에 추가.
    public void AddItemEquip()
    {

    }
}
