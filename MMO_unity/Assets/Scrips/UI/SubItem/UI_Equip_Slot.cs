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

    public void ItemClick(PointerEventData evt)
    {
        Inventory playerInven = _player.GetComponent<Inventory>();
        Equipment playerEquip = _player.GetComponent<Equipment>();

        Debug.Log($"{gameObject.name}");
        Debug.Log($"{playerEquip.wearItems[gameObject.name]}");
        int removeItemID = playerEquip.wearItems[gameObject.name];
        if (removeItemID == 0) {
            Debug.Log("No item clicked");
            return;
        }
        
        //인벤토리에 추가
        if(!playerInven.Add(removeItemID))
        {
            Debug.Log("No space to UnAttach");
            return;
        }

        //장비창의 정보를 삭제 시켜줌
        playerEquip.wearItems[gameObject.name] = 0;


        //아이콘 삭제.
        Debug.Log(gameObject.transform.parent.name);
        Transform equipItemIcon = gameObject.transform.GetChild(0);
        Debug.Log(equipItemIcon.name);
        if (equipItemIcon.GetComponent<Image>().sprite == null) {
            Debug.Log($"{equipItemIcon} : No item clicked");
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
