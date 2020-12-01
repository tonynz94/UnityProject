using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public Transform _rightSocket;
    public static Object _equipedWeapon;

    private void Start()
    {
        Managers.Equip.OnWeaponChange -= OnWeaponEquip;
        Managers.Equip.OnWeaponChange += OnWeaponEquip;
        _equipedWeapon = null;
    }
    public void OnWeaponEquip(int weaponId)
    {
        if (_equipedWeapon != null)
            OnWeaponRemoveEquip();

        string _tempWeapon = Managers.Data.ItemDict[weaponId].name;       
        _equipedWeapon = Instantiate(Resources.Load($"Prefabs/Items/Equip/{_tempWeapon}"), _rightSocket);      
        gameObject.GetComponent<PlayerController>().State = Define.State.Idle;
    }

    public void OnWeaponRemoveEquip()
    {
        Destroy(_equipedWeapon);
        _equipedWeapon = null;
    }
}
