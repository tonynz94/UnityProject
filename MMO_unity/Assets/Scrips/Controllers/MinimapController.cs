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

    void LateUpdate()
    {
        Vector3 newPos = _player.transform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        //transform.rotation = Quaternion.Euler(90f, _player.transform.eulerAngles.y, 0f);
    }
}
