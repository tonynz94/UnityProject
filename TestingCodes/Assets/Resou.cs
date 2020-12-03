using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resou : MonoBehaviour
{
    public float ballSpeed = 1f / 100f;
    float moveTimer = 0;
    public bool _move;
    public float height;
    public Transform _target;

    float distance;
    float movePerSecond;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        _move = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_move)
        {
            float speed = Mathf.Clamp(movePerSecond * Time.deltaTime,  0,  (_target.position - transform.position).magnitude);
            //Debug.Log((_target.position - transform.position).magnitude);
            Debug.Log(speed);
            if ((_target.position - transform.position).magnitude < 0.01f)
            {
                _move = false;
            }
            else
            {
                Debug.Log("Move");
                this.transform.position += dir * (speed);
            }
        }
    }

    public void Move()
    {
        distance = (_target.position - transform.position).magnitude;

        movePerSecond = distance / 10.0f;
        dir = (_target.position - transform.position).normalized;
        _move = true;
    }


}
