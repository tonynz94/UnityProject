using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Quest : MonoBehaviour
{
    public GameObject ContentFrame;

    public GameObject Contents;
    GameObject isEmptyText;

    public List<UI_Quest_Content> contentsList = new List<UI_Quest_Content>();

    // Start is called before the first frame update

    private void Start()
    {
        UIController.instance.onQuestFrameOn -= ContentFrameOn;
        UIController.instance.onQuestFrameOn += ContentFrameOn;

        UIController.instance.onQuestFrameOff -= ContentFrameOff;
        UIController.instance.onQuestFrameOff += ContentFrameOff;

        Managers.Quest.onQuestContentTextUpdate -= UpdateQuestContent;
        Managers.Quest.onQuestContentTextUpdate += UpdateQuestContent;

        Managers.Quest.onQuestContentsUpdateCallBack -= OnQuestContentsUpdate;
        Managers.Quest.onQuestContentsUpdateCallBack += OnQuestContentsUpdate;

        Managers.Quest.onQuestUpdateCallBack -= QuestFrameTurnOn;
        Managers.Quest.onQuestUpdateCallBack += QuestFrameTurnOn;

        Managers.Quest.onQuestFinshProgressTextUpdate -= ProgressFinishText;
        Managers.Quest.onQuestFinshProgressTextUpdate += ProgressFinishText;
    }

    //퀘스트 디테일이 켜질때 실행.
    public void ContentFrameOn(GameObject frame)
    {
        ContentFrame = frame;
        Contents = Util.FindChild(ContentFrame, "Contents", true);
        isEmptyText = Util.FindChild(ContentFrame, "QuestEmptyText", true);

        OnQuestContentsUpdate();
    }

    //퀘스트 디테일이 꺼질때 실행
    public void ContentFrameOff()
    {
        ContentFrame = null;
        isEmptyText = null;
        contentsList.Clear();
    }
       
    //열린 상태에서 업데이트가 있을 시 실행
    public void OnQuestContentsUpdate()
    {
        if (isEmptyText != null)
        {
            if (Managers.Quest.QuestActive.Count + Managers.Quest.ReachQuest.Count >= 1)
                isEmptyText.SetActive(false);
            else
                isEmptyText.SetActive(true);
        }

        if (contentsList.Count != 0)
        {
            foreach(UI_Quest_Content temp in contentsList)
                Destroy(temp.gameObject);

            contentsList.Clear();
        }
        
        //진행하고 있는 퀘스트 동적 생성
        foreach (int questID in Managers.Quest.QuestActive)
        {
            GameObject contentObject = Managers.Resource.Instantiate("UI/Popup/QuestPopup/SubQuest/QuestContect", Contents.transform);
            UI_Quest_Content questContent = contentObject.GetOrAddComponent<UI_Quest_Content>();
            
            questContent.QuestAdd(questID);
            contentsList.Add(questContent);
        }

        //진행하은 하고있지만 완료한 퀘스트 동적 생성
        foreach (int questID in Managers.Quest.ReachQuest)
        {
            GameObject contentObject = Managers.Resource.Instantiate("UI/Popup/QuestPopup/SubQuest/QuestContect", Contents.transform);
            UI_Quest_Content questContent = contentObject.GetOrAddComponent<UI_Quest_Content>();
            questContent.CompleteProgressText();

            questContent.QuestAdd(questID);
            contentsList.Add(questContent);
        }
    }

    public void UpdateQuestContent(int questID)
    {
        foreach(UI_Quest_Content content in contentsList)
        {
            if (content.thisQuestID == questID)
                content.UpdateContentRequest();
        }
    }


    //디테일 창이 켜져있는 상태에서 퀘스트를 받을 시 실행
    public void QuestFrameTurnOn(int questID)
    {
        isEmptyText = Util.FindChild(ContentFrame, "QuestEmptyText", true);

        if (isEmptyText  != null && Managers.Quest.QuestActive.Count >= 1)
            isEmptyText.SetActive(false);

        GameObject contentObject = Managers.Resource.Instantiate("UI/Popup/QuestPopup/SubQuest/QuestContect", Contents.transform);
        UI_Quest_Content questContent = contentObject.GetOrAddComponent<UI_Quest_Content>();
        
        questContent.QuestAdd(questID);
    }
    
    public void ProgressFinishText(int questID)
    {
        foreach (UI_Quest_Content content in contentsList)
        {
            if (questID == content.thisQuestID)
                content.CompleteProgressText();
        }
    }
}
