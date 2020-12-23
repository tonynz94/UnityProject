using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest_Content : UI_Base
{
    public int thisQuestID;
    GameObject _rewardItem;

    GameObject _questTitle;
    GameObject _questDescription;
    GameObject _questGoal;
    GameObject _progress;
    GameObject _rewardText;
    GameObject _rewardBackGround;

    string goalText;

    public enum GameObjects
    {
        QuestTitle,
        QuestDescription,
        QuestGoal,
        Progress,
        RewardText,
        RewardBackGround,
    }

    public void Awake()
    {
        Bind<GameObject>(typeof(GameObjects));
        goalText = "";

        _rewardItem = Resources.Load<GameObject>("Prefabs/UI/Popup/QuestPopup/SubQuest/RewardItem");
        _questTitle = Get<GameObject>((int)GameObjects.QuestTitle);
        _questDescription = Get<GameObject>((int)GameObjects.QuestDescription);
        _questGoal = Get<GameObject>((int)GameObjects.QuestGoal);
        _progress = Get<GameObject>((int)GameObjects.Progress);
        _rewardText = Get<GameObject>((int)GameObjects.RewardText);
        _rewardBackGround = Get<GameObject>((int)GameObjects.RewardBackGround);
    }

    public void QuestAdd(int id)
    {
        thisQuestID = id;
        _questTitle.GetOrAddComponent<Text>().text = $"{Managers.Data.QuestDict[id].title}";
        _questDescription.GetOrAddComponent<Text>().text = $"{Managers.Data.QuestDict[id].description}";

        foreach (int ItemID in Managers.Data.QuestDict[id].itemReward)
        {
            Sprite sprite = null;
            if (Managers.Data.ItemDict.TryGetValue(ItemID,out Data.Item itemValue))
                sprite = itemValue.icon;

            if (Managers.Data.ConsumeItemDict.TryGetValue(ItemID, out Data.ConsumeItem consumeValue))
                sprite = consumeValue.icon;

            GameObject reward = Managers.Resource.Instantiate("UI/Popup/QuestPopup/SubQuest/RewardItem", _rewardBackGround.transform);
            reward.GetOrAddComponent<Image>().sprite = sprite;
        }
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

    public void CompleteProgressText()
    {
        _progress.GetOrAddComponent<Text>().color = Color.green;
        _progress.GetOrAddComponent<Text>().text = "완료";
    }

    public override void Init()
    {
        
    }

}
