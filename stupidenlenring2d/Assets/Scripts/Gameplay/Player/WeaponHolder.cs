using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private WeaponData currentWeapon;
    private PlayerAttribute playerAttribute;
    private PlayerMoving playerMoving;
    private void Awake(){
        playerAttribute = GetComponentInParent<PlayerAttribute>();
        playerMoving = GetComponentInParent<PlayerMoving>();
    }
    private void OnEnable(){
        PlayerManager.Instance.onChangeEquipment += OnEquipmentChanged;
    }
    private void Start(){
        HoldWeapon();
    }
    private void OnDisable(){
        PlayerManager.Instance.onChangeEquipment -= OnEquipmentChanged;
    }

    private void OnEquipmentChanged(object sender, EventArgs e)
    {
        HoldWeapon();
    }
    private void HoldWeapon(){
        if (PlayerManager.Instance.GetEquipmentByID(playerAttribute.weapon1?.eid))
            SwitchWeapon(PlayerManager.Instance.GetEquipmentByID(playerAttribute.weapon1.eid).weapon);
        else UnequipWeapon();
    }
    public void SwitchWeapon(WeaponData newWeapon){
        UnequipWeapon();
        currentWeapon = newWeapon;
        WeaponPhysic ph = Instantiate(newWeapon.weaponObject, transform).GetComponent<WeaponPhysic>();
        playerAttribute.UpdateWeapon(newWeapon, ph);
        ph.SetMaster(playerMoving);
    }
    public void UnequipWeapon(){
        if(currentWeapon != null){
            currentWeapon = null;
            playerAttribute.OnWeaponUnequipped();
        }
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
    }
}
