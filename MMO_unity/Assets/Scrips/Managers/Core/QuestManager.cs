using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    public List<Data.Quest> questActive = new List<Data.Quest>();

    public void QuestAdd(int QuestID)
    {
        questActive.Add(Managers.Data.QuestDict[QuestID]);
        Debug.Log("퀘스트 추가");
    }

    public bool IsReached(Data.Quest ActiveQuest)
    {
        foreach (KeyValuePair<int, int> require in ActiveQuest.questGoal.requiredAmount)
        {
            if (!(require.Value <= ActiveQuest.questGoal.currentAmount[require.Key]))
            {
                return false;
            }
        }
        return true;
    }


    public void CollectOrKill(int Id)
    {
        if (questActive.Count == 0)
        {
            Debug.Log("수락한 퀘스트가 없습니다.");
            return;
        }

        foreach (Data.Quest quest in questActive)
        {
            if(quest.questGoal.currentAmount.TryGetValue(Id, out int value))
            {
                value++;
                if(IsReached(quest))
                {
                    CompleteQuest();
                }                           
            }
        }
    }

    public void CompleteQuest()
    {
        Debug.Log("퀘스트 완료");
    }

}
