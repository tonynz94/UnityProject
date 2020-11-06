using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{

    GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = Managers.Game.GetPlayer();    
    }

    void Update()
    {
        Vector3 pos = _player.transform.position;
        gameObject.transform.position = new Vector3(pos.x, gameObject.transform.position.y, pos.z);
    }


}
