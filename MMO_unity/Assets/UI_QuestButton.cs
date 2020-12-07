using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_QuestButton : MonoBehaviour
{
    public GameObject _questDetail;
    bool questDetail = false;
   
    public void QuestDetailTurnOnOff()
    {
        questDetail = !questDetail;
        _questDetail.SetActive(questDetail);

    }
}
