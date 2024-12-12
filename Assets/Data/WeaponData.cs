using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string weaponID;
    public string weaponType;
    public GameObject weaponObject;
    public Equipment equipment;
    [Header("Attack")]
    public float dmgMultiplier;
    public int normalAttackStamina; //sword: 20
    public int chargeAttackStamina; //sword: 60
    [Header("Block Resistance")]
    public int physicalRes;
    public int fireRes;
    public int iceRes;
    public int lightningRes;
    public int magicRes;
    [Header("Weapon Skill")]
    public PlayerSkill weaponSkill;
}
