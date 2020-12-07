using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest_Content : UI_Base
{
    GameObject _questTitle;
    GameObject _questGoal;
    GameObject _progress;
    GameObject _rewardText;
    GameObject _rewordBackGround;

    public enum GameObjects
    {
        QuestTitle,
        QuestGoal,
        Progress,
        RewardText,
        RewordBackGround,
    }

    public void Awake()
    {
        Bind<GameObject>(typeof(GameObjects));

        _questTitle = Get<GameObject>((int)GameObjects.QuestTitle);
        _questGoal = Get<GameObject>((int)GameObjects.QuestGoal);
        _progress = Get<GameObject>((int)GameObjects.Progress);
        _rewardText = Get<GameObject>((int)GameObjects.RewardText);
        _rewordBackGround = Get<GameObject>((int)GameObjects.RewordBackGround);
    }

    public void QuestAdd(int id)
    {
        _questTitle.GetOrAddComponent<Text>().text = $"{Managers.Data.QuestDict[id].title}";
        _questGoal.GetOrAddComponent<Text>().text = $"{Managers.Data.QuestDict[id].description}";
    }

    public override void Init()
    {
        
    }

}
