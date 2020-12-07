using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Quest : MonoBehaviour
{
    List<int> questList = new List<int>();
    public GameObject contentFrame;
    public GameObject isEmptyText;

    // Start is called before the first frame update
    void Start()
    {        
        Managers.Quest.onQuestAddCallBack -= QuestAdd;
        Managers.Quest.onQuestAddCallBack += QuestAdd;
        CheckQuestExist();


    }

    public void QuestAdd(int mQuestId)
    {
        CheckQuestExist();
        questList.Add(mQuestId);
        GameObject contentObject = Managers.Resource.Instantiate("UI/Popup/QuestPopup/SubQuest/QuestContect", contentFrame.transform);
        UI_Quest_Content content = contentObject.GetOrAddComponent<UI_Quest_Content>();
        content.QuestAdd(mQuestId);
    }

    public void CheckQuestExist()
    {
        if (questList.Count == 0)
            isEmptyText.SetActive(true);
        else
            isEmptyText.SetActive(false);
    }
}
