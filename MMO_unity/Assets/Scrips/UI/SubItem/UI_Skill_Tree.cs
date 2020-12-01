using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skill_Tree : UI_Base
{
    UI_SkillTree _skillTree;
    public int _slotSkillId;
    public int SlotSkillId{ get { return _slotSkillId; } set { _slotSkillId = value; } }
    
    Image _skillImage;
    Button _skillUpBtn;
    Text _SkillPointText;


    public enum GameObjects
    {
        SkillSlot,
        UpButton,
        SkillPointText
    }
    
    // Start is called before the first frame update
    void Awake()
    {

        Bind<GameObject>(typeof(GameObjects));

        _skillImage = Get<GameObject>((int)GameObjects.SkillSlot).GetComponent<Image>();
        _skillUpBtn = Get<GameObject>((int)GameObjects.UpButton).GetComponent<Button>();   
        _SkillPointText = Get<GameObject>((int)GameObjects.SkillPointText).GetComponent<Text>();
        _skillTree = GetComponentInParent<UI_SkillTree>();
    }

    //스킬창을 열었을 때.
    void Start()
    {
        int skillPoint = Managers.Skill.GetSkillPoint(_slotSkillId);

        if (skillPoint == 5)
            SkillMax();
        else
            _SkillPointText.text = $"{Managers.Skill.GetSkillPoint(_slotSkillId)}";
    }

    public void SkillOn()
    {
        _skillUpBtn.interactable = true;
        _skillImage.color = new Color(_skillImage.color.r, _skillImage.color.g, _skillImage.color.b, 1.0f);
    }

    public void SkillOff()
    {
        _skillUpBtn.interactable = false;
        _skillImage.color =new Color(_skillImage.color.r, _skillImage.color.g, _skillImage.color.b, 0.3f);
    }

    public void SkillMax()
    {
        _SkillPointText.text = "MAX";
        _skillImage.color = new Color(_skillImage.color.r, _skillImage.color.g, _skillImage.color.b, 1.0f);
        _skillUpBtn.interactable = false;
    }

    public void UpButtonClick()
    {
        if (!Managers.Skill.SkillUp(SlotSkillId))
            return;

        _skillTree.UpdateSkillPointText();

        if (Managers.Skill.SkillListAndPoint[_slotSkillId] >= 5)
        {
            SkillMax();
            return;
        }

        _SkillPointText.text = $"{Managers.Skill.SkillListAndPoint[_slotSkillId]}";     
    }
    // Update is called once per frame

    public override void Init()
    {

    }
}
