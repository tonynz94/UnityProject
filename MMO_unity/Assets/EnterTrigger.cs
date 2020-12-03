using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
    public bool enterOnce = false;
    public GameObject _boss;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !enterOnce)
        {
            Debug.Log("Enter");
            if(!enterOnce)
            {
                enterOnce = true;
                Managers.Game.BossDoorOpenClose(Define.BossDoor.Close);
                _boss.GetComponent<BossController>().StartBossStage();
            }                     
        }
    }
}
