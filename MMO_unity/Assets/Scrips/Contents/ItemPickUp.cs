﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    float radius =1.0f;

    private void Start()
    {
        if(gameObject.GetComponent<Item>() == null)
        {
            Debug.Log("cant found Item");
        }
    }

    void Update()
    {
        float distance = (Managers.Game.GetPlayer().transform.position - gameObject.transform.position).magnitude;
        Debug.Log(distance);
        if(distance <= radius)
        {
            PickUp();
        }
    }

    //자동으로 실행 해주는 함수.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }

    void PickUp()
    {
        Debug.Log("Pick it up");
        FindObjectOfType<UI_Inven>().Add(gameObject.GetComponent<Item>());
        Destroy(gameObject);
    }
}
