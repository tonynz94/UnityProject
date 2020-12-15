using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    public Action onQuestContentsUpdateCallBack;

    public Action<int> onQuestUpdateCallBack;
    public Action<int> onQuestContentTextUpdate;
    public Action<int> onQuestFinshProgressTextUpdate;

    public List<int> QuestActive = new List<int>();  //진행 중인 퀘스트
    public List<int> ReachQuest = new List<int>();  //진행 중 완료된 퀘스트 
    public List<int> FinshQuest = new List<int>();   //완전히 완료된 퀘스트

    //퀘스트가 추가되었을때.
    public void UpdateQuestAdd(int QuestID)
    {

        Debug.Log("진행중인 퀘스트로 추가~~");
        QuestActive.Add(QuestID);

        //켜져 있는 상태일때.
        if (UIController.instance.QuestFrame != null)
            onQuestUpdateCallBack.Invoke(QuestID);
    }

    //퀘스트를 완료하여 삭제했을 때.
    public void UpdateQuestRemove()
    {
        Debug.Log("퀘스트 창에서 삭제하기");
        if (UIController.instance.QuestFrame != null)
            onQuestContentsUpdateCallBack();
    }

    //완료했는지 확인
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

    //수집했을때 실행
    public void IsCollectOrKill(int Id)
    {
        Debug.Log("방금 수확한 아이템 ID" + Id);
        if (QuestActive.Count == 0)
        {
            Debug.Log("수락한 퀘스트가 없습니다.");
            return;
        }

        foreach (int questID in QuestActive.ToArray())
        {       
            if (Managers.Data.QuestDict[questID].questGoal.currentAmount.TryGetValue(Id, out int value))
            {
                Managers.Data.QuestDict[questID].questGoal.currentAmount[Id] += 1;
                onQuestContentTextUpdate(questID);
                if (IsReached(questID))
                {
                    if (UIController.instance.QuestFrame != null)
                        onQuestFinshProgressTextUpdate.Invoke(questID);
                    CompleteQuest(questID);
                }                           
            }
        }
    }

    public void CompleteQuest(int questID)
    {
        Debug.Log("Reached로 옮김");
        //진행중인 퀘스트가 완료 되면 추가하기.
        if (QuestActive.Remove(questID))
        {
            Debug.Log("진행중인 퀘스트 삭제");
            for (int i = 0; i < QuestActive.Count; i++)
            {
                Debug.Log(QuestActive[i]);
            }
            
            Managers.Sound.Play("Sounds/GameSound/MissionCompleteCheck");
            ReachQuest.Add(questID);
            Debug.Log("퀘스트가 완료로 옮김 상태");
        }
    }

    public void FinishQuest(int questID)
    {
        if (ReachQuest.Remove(questID))
        {
            FinshQuest.Add(questID);
            FinshQuestReward(questID);
            UpdateQuestRemove();
        }
    }

    public void FinshQuestReward(int questID)
    {
        //보상을 줄때 퀘스트가 요구하는 아이템을 삭제.
        foreach(int ItemID in Managers.Data.QuestDict[questID].questGoal.requiredAmount.Keys)
        {
            for (int i = 0; i < Managers.Data.QuestDict[questID].questGoal.requiredAmount[ItemID]; i++)
            {
                Managers.Inven.RemoveByItemID(ItemID);
            }
        }

        //아이템 보상
        foreach(int ItemID in Managers.Data.QuestDict[questID].itemReward)
        {
            Managers.Inven.Add(ItemID,Define.InvenType.Equipments);
        }

    }

}
