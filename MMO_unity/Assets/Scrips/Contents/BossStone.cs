using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStone : MonoBehaviour
{
    Rigidbody _rigid;
    bool _isShoot;
    float _angluerPower = 2.0f;
    float _scaleValue = 0.1f;
    
    public int _damage;
    // Start is called before the first frame update
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(2.2f);
        _isShoot = true;
    }

    IEnumerator GainPower()
    {
        while(!_isShoot)
        {
            _angluerPower += 0.02f;
            _scaleValue += 0.01f;
            transform.localScale = Vector3.one * _scaleValue;
            _rigid.AddTorque(transform.right * _angluerPower, ForceMode.Acceleration);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("MainPlayer"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            Destroy(gameObject);
        }
    }
}
