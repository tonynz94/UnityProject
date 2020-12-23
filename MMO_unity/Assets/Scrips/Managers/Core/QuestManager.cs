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

    //진행 중인 퀘스트를 저장할 리스트
    public List<int> QuestActive = new List<int>();
    
    //진행 중 완료된 퀘스트를 저장할 리스트
    public List<int> ReachQuest = new List<int>();
    
    //완전히 완료된 퀘스트를 저장할 리스트
    public List<int> FinshQuest = new List<int>();   

    //플레이가 대화중 퀘스트가 있는 NPC의 
    //Yes버튼을 누르면 퀘스트ID를 받아 
    //진행중인 퀘스트 리스트에 추가합니다.
    public void UpdateQuestAdd(int QuestID)
    {
        QuestActive.Add(QuestID);

        //만약 상세페이지가 켜져있다면,
        //상세페이지에 동적으로 올라 갈 퀘스트안내도를 업데이트 해줍니다.
        if (UIController.instance.QuestFrame != null)
            onQuestUpdateCallBack.Invoke(QuestID);
    }

    //퀘스트를 완료하여
    //퀘스트 상세페이지가 켜진 경우 
    //완료한 상세페이지의 내용을 삭제해줍니다.
    public void UpdateQuestRemove()
    {
        if (UIController.instance.QuestFrame != null)
            onQuestContentsUpdateCallBack();
    }

    //퀘스트의 조건에 충족했는지 확인해주는 함수
    public bool IsReached(int ActiveQuestID)
    {
        //진행중인 퀘스트의 리스트에서 퀘스트 ID값을 이용하여 
        //퀘스트 데이터 중 requiredAmount(퀘스트를 완료하는데 필요한 양)을 넘었는지 검사
        foreach (KeyValuePair<int, int> require in Managers.Data.QuestDict[ActiveQuestID].questGoal.requiredAmount)
        {
            //True -> 아직 퀘스트가 완료하지 못한 것
            //False -> 퀘스트가 완료한 것
            if (!(require.Value <= Managers.Data.QuestDict[ActiveQuestID].questGoal.currentAmount[require.Key]))
            {
                return false;
            }
        }
        return true;
    }

    //몬스터를 죽이거나, 수집했을때 실행되는 함수.
    public void IsCollectOrKill(int Id)
    {
        //퀘스트가 하나도 없다면 함수를 종료시켜준다.
        if (QuestActive.Count == 0)
        {
            return;
        }

        //진행 중인 퀘스트의 ID값을 하나씩 가져온다.
        foreach (int questID in QuestActive.ToArray())
        {       
            //만약 퀘스트 중 필요한 아이템이나, 몬스터를 죽인게 있는지 검사 
            //True -> 퀘스트에 필요한 아이템이나 몬스터를 수집하거나 죽였다는 것
            if (Managers.Data.QuestDict[questID].questGoal.currentAmount.TryGetValue(Id, out int value))
            {
                //퀘스트의 필요한 조건의 값을 1 증가 시켜 IsReached함수에서 조건을 충족했는지 검사합니다.
                Managers.Data.QuestDict[questID].questGoal.currentAmount[Id] += 1;
                onQuestContentTextUpdate(questID);
                //True -> 조건을 모두 충족한 것.
                if (IsReached(questID))
                {
                    //상세페이지가 켜져 있다면 퀘스트의 텍스트를 "진행중"에서 -> "완료"로 바꿔줍니다.
                    if (UIController.instance.QuestFrame != null)
                        onQuestFinshProgressTextUpdate.Invoke(questID);
                    CompleteQuest(questID);
                }                           
            }
        }
    }


    //퀘스트가 완료 되었을때 실행(보상은 아직 받지 않는 상태)
    public void CompleteQuest(int questID)
    {
        //진행중인 퀘스트가 완료 되면 진행중 리스트에서 삭제해주고 
        //완료한 리스트에 추가해줍니다.
        if (QuestActive.Remove(questID))
        {
            //Mission 소리르 재생시킵니다.
            Managers.Sound.Play("Sounds/GameSound/MissionCompleteCheck");
            //완료한 리스트에 추가하기.
            ReachQuest.Add(questID);
        }
    }

    //완료한 리스트에 대한 보상을 받을때 실행되는 함수
    //(퀘스트 조건을 충족하여 해당 NPC 말을 걸었을때 실행되는 것) 
    public void FinishQuest(int questID)
    {
        //보상을 받아 줍니다.
        if (ReachQuest.Remove(questID))
        {
            FinshQuest.Add(questID);
            FinshQuestReward(questID);
            UpdateQuestRemove();
        }
    }

    //퀘스트를 완료하여 보상 받을 아이템 여러개를 반복문을 통해 
    //인벤토리에 추가해주는 함수
    public void FinshQuestReward(int questID)
    {
        //보상을 줄때 퀘스트가 요구하는 아이템은 삭제 해줍니다..
        foreach(int ItemID in Managers.Data.QuestDict[questID].questGoal.requiredAmount.Keys)
        {
            for (int i = 0; i < Managers.Data.QuestDict[questID].questGoal.requiredAmount[ItemID]; i++)
            {
                Managers.Inven.RemoveByItemID(ItemID);
            }
        }

        //여러개의 보상 아이템 인벤토리에 반복문을 통해 넣어줍니다.
        foreach(int ItemID in Managers.Data.QuestDict[questID].itemReward)
        {
            Managers.Inven.Add(ItemID,Define.InvenType.Equipments);
        }
    }
}
