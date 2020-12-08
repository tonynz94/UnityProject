using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    public Action onQuestAddCallBack;
    public Action<int> OnQuestTurnOnCallBack;
    public Action<int> OnQuestContentTextUpdate;

    public List<int> questActive = new List<int>();  //진행 중인 퀘스트
    public List<int> ReachQuest = new List<int>();  //진행 중 완료된 퀘스트 
    public List<int> FinshQuest = new List<int>();   //완전히 완료된 퀘스트

    public void QuestAdd(int QuestID)
    {
        questActive.Add(QuestID);

        //켜져 있는 상태일때.
        if (UIController.instance.QuestFrame != null)
            OnQuestTurnOnCallBack.Invoke(QuestID);
    }

    //재료들을 다 확인.
    public bool IsReached(int ActiveQuestID)
    {
        foreach (KeyValuePair<int, int> require in Managers.Data.QuestDict[ActiveQuestID].questGoal.requiredAmount)
        {
            if (!(require.Value <= Managers.Data.QuestDict[ActiveQuestID].questGoal.currentAmount[require.Key]))
            {
                return false;
            }
        }
        return true;
    }


    public void CollectOrKill(int Id)
    {
        Debug.Log("방금 수확한 아이템 ID" + Id);
        if (questActive.Count == 0)
        {
            Debug.Log("수락한 퀘스트가 없습니다.");
            return;
        }

        foreach (int questID in questActive.ToArray())
        {       
            if (Managers.Data.QuestDict[questID].questGoal.currentAmount.TryGetValue(Id, out int value))
            {
                Managers.Data.QuestDict[questID].questGoal.currentAmount[Id] += 1;
                OnQuestContentTextUpdate(questID);
                Debug.Log("값에 업데이트 함.");
                if (IsReached(questID))
                {
                    Debug.Log("완료한 상태");
                    CompleteQuest(questID);
                }                           
            }
        }
    }

    public void CompleteQuest(int questID)
    {
        Debug.Log("Reached로 옮김");
        //진행중인 퀘스트가 완료 되면 추가하기.
        if (questActive.Remove(questID))
        {
            ReachQuest.Add(questID);
            Debug.Log("퀘스트가 완료로 옮김 상태");
        }
    }

}
