using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest_Content : UI_Base
{
    public int thisQuestID;

    GameObject _questTitle;
    GameObject _questDescription;
    GameObject _questGoal;
    GameObject _progress;
    GameObject _rewardText;
    GameObject _rewordBackGround;

    string goalText;

    public enum GameObjects
    {
        QuestTitle,
        QuestDescription,
        QuestGoal,
        Progress,
        RewardText,
        RewordBackGround,
    }

    public void Awake()
    {
        Bind<GameObject>(typeof(GameObjects));
        goalText = "";

        _questTitle = Get<GameObject>((int)GameObjects.QuestTitle);
        _questDescription = Get<GameObject>((int)GameObjects.QuestDescription);
        _questGoal = Get<GameObject>((int)GameObjects.QuestGoal);
        _progress = Get<GameObject>((int)GameObjects.Progress);
        _rewardText = Get<GameObject>((int)GameObjects.RewardText);
        _rewordBackGround = Get<GameObject>((int)GameObjects.RewordBackGround);
    }

    public void QuestAdd(int id)
    {
        thisQuestID = id;
        _questTitle.GetOrAddComponent<Text>().text = $"{Managers.Data.QuestDict[id].title}";
        _questDescription.GetOrAddComponent<Text>().text = $"{Managers.Data.QuestDict[id].description}";

        UpdateContentRequest();
    }

    public void UpdateContentRequest()
    {
        goalText = "";
        foreach (KeyValuePair<int, int> require in Managers.Data.QuestDict[thisQuestID].questGoal.requiredAmount)
        {
            goalText += $"{Managers.Data.OtherItemDict[require.Key].name} ({Managers.Data.QuestDict[thisQuestID].questGoal.currentAmount[require.Key]}/{require.Value}) ";
        }
        _questGoal.GetOrAddComponent<Text>().text = $"{goalText}";
    }

    public override void Init()
    {
        
    }

}
