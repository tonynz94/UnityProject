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
        UIController.instance.onQuestFrame -= ContentFrameOn;
        UIController.instance.onQuestFrame += ContentFrameOn;

        UIController.instance.offQuestFrame -= ContentFrameOff;
        UIController.instance.offQuestFrame += ContentFrameOff;

        Managers.Quest.OnQuestContentTextUpdate -= UpdateQuestContent;
        Managers.Quest.OnQuestContentTextUpdate += UpdateQuestContent;

        Managers.Quest.onQuestAddCallBack -= QuestDetailOn;
        Managers.Quest.onQuestAddCallBack += QuestDetailOn;

        Managers.Quest.OnQuestTurnOnCallBack -= QuestFrameTurnOn;
        Managers.Quest.OnQuestTurnOnCallBack += QuestFrameTurnOn;
    }

    //퀘스트 디테일이 켜질때 실행.
    public void ContentFrameOn(GameObject temp)
    {
        ContentFrame = temp;
        Contents = Util.FindChild(ContentFrame, "Contents", true);
        isEmptyText = Util.FindChild(ContentFrame, "QuestEmptyText", true);

        if (isEmptyText != null)
        {
            if (Managers.Quest.questActive.Count >= 1)
                isEmptyText.SetActive(false);
        }
    }

    //퀘스트 디테일이 꺼질때 실행
    public void ContentFrameOff()
    {
        ContentFrame = null;
        isEmptyText = null;
    }


    public void QuestDetailOn()
    {     
        foreach (int questID in Managers.Quest.questActive)
        {
            GameObject contentObject = Managers.Resource.Instantiate("UI/Popup/QuestPopup/SubQuest/QuestContect", Contents.transform);
            UI_Quest_Content questContent = contentObject.GetOrAddComponent<UI_Quest_Content>();
            
            Debug.Log(questID);
            questContent.QuestAdd(questID);
            contentsList.Add(questContent);
        }
    }

    public void UpdateQuestContent(int id)
    { 
        foreach(UI_Quest_Content content in contentsList)
        {
            Debug.Log($"{content.thisQuestID} == {id}");
            if (content.thisQuestID == id)
                content.UpdateContentRequest();
        }
    }


    //켜져있는 상태에서 퀘스트를 받을 시 실행
    public void QuestFrameTurnOn(int questID)
    {
        isEmptyText = Util.FindChild(ContentFrame, "QuestEmptyText", true);

        if (isEmptyText  != null && Managers.Quest.questActive.Count >= 1)
            isEmptyText.SetActive(false);

        GameObject contentObject = Managers.Resource.Instantiate("UI/Popup/QuestPopup/SubQuest/QuestContect", Contents.transform);
        UI_Quest_Content questContent = contentObject.GetOrAddComponent<UI_Quest_Content>();
        
        questContent.QuestAdd(questID);
    }
}
