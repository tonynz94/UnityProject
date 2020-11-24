using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public Transform _rightSocket;
    public Object _equipedWeapon;

    private void Start()
    {
        Managers.Equip.OnWeaponChange -= OnWeaponEquip;
        Managers.Equip.OnWeaponChange += OnWeaponEquip;
        _equipedWeapon = null;
    }
    void OnWeaponEquip(int weaponId)
    {
        if (_equipedWeapon != null)
            OnWeaponRemoveEquip();

        string _tempWeapon = Managers.Data.ItemDict[weaponId].name;
        _equipedWeapon = Resources.Load($"Prefabs/Items/Equip/{_tempWeapon}");

        Instantiate(_equipedWeapon, _rightSocket);
    }

    void OnWeaponRemoveEquip()
    {
        Destroy(_equipedWeapon);
        _equipedWeapon = null;
    }
}
