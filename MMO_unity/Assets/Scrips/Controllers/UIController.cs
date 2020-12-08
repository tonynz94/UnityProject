using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region singleton
    // Start is called before the first frame update

    //클래스 명으로 접근 가능.
    public static UIController instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More then one instance of UI controller found!");
            return;
        }
        instance = this;
    }
    #endregion

    UI_Inventory _Inven;
    UI_Equipment _Equip;
    UI_SkillTree _SkillTree;


    [SerializeField]
    GameObject QuestParent;
    [SerializeField]
    public GameObject QuestFrameLoad;
    public GameObject QuestFrame;

    public Action<GameObject> onQuestFrame;
    public Action offQuestFrame;
    private void Start()
    {
        
        QuestFrameLoad = Resources.Load<GameObject>("Prefabs/UI/Popup/QuestFrame");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowEquipment();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowSkillTree();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    public void CloseUI()
    {
        UI_Popup LastPopUp = Managers.UI.PeekUI();
        if (LastPopUp != null)
        {
            string temp = LastPopUp.PopUpName();
            Debug.Log($"{temp}");
            if (LastPopUp.ClosePopupUI())
            {
                //하드코딩 나중에 수정하기
                Debug.Log($"{temp} == Inven: {temp == "Inven" }");
                if (temp == "Inven")
                {
                    Debug.Log("인벤토리");
                    _Inven = null;
                }
                else if (temp == "Equip")
                {
                    _Equip = null;
                }

                else if (temp == "SkillTree")
                {
                    _SkillTree = null;
                }
            }
        }
    }

    public void ShowQuestDetail()
    {
        //켜질때
        if (QuestFrame == null)
        {
            //껏다가 다시 켜질때 실행되는 부분
            QuestFrame = Instantiate<GameObject>(QuestFrameLoad, QuestParent.transform);
            onQuestFrame.Invoke(QuestFrame);
            Managers.Quest.onQuestAddCallBack();
            
        }
        //꺼질때
        else
        {
            Destroy(QuestFrame);
            QuestFrame = null;
            offQuestFrame.Invoke();
        }

    }

    public void ShowInventory()
    {
        if (_Inven == null)
            _Inven = Managers.UI.ShowPopupUI<UI_Inventory>("UI_Inven");
        else
        {
            if (_Inven.ClosePopupUI())
                _Inven = null;
        }
    }

    public void ShowEquipment()
    {
        if (_Equip == null)
        {
            _Equip = Managers.UI.ShowPopupUI<UI_Equipment>("UI_Equipment");
        }
        else
        {
            if (_Equip.ClosePopupUI())
                _Equip = null;
        }
    }

    public void ShowSkillTree()
    {
        if (_SkillTree == null)
        {
            _SkillTree = Managers.UI.ShowPopupUI<UI_SkillTree>("UI_SkillTree");
        }
        else
        {
            if (_SkillTree.ClosePopupUI())
                _SkillTree = null;
        }
    }
}
