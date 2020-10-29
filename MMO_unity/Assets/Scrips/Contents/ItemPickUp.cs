using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    float radius =1.0f;

    [SerializeField]
    public Item item;

    GameObject player;


    private void Start()
    {
        player = Managers.Game.GetPlayer();
    }

    void Update()
    {
        float distance = (player.transform.position - gameObject.transform.position).magnitude;
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
        bool wasPickedUp = player.GetComponent<Inventory>().Add(item.ItemTemplateId);
        
        if(wasPickedUp)
            Destroy(gameObject);
    }
}
