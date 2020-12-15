using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
    public bool enterOnce = false;
    public GameObject _boss;
    public GameObject _bossHPBar;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !enterOnce)
        {
            Debug.Log("Enter");
            if(!enterOnce)
            {
                enterOnce = true;
                Managers.Sound.Play("Sounds/GameSound/DragonNest", Define.Sound.Bgm, 0.45f);
                Managers.Game.BossDoorOpenClose(Define.BossDoor.Close);
                Managers.Resource.Instantiate("UI/Popup/UI_BossHPBar");
                _boss.GetComponent<BossController>().StartBossStage();
            }                     
        }
    }
}
