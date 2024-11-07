using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Equipment : ScriptableObject
{
    public string eid;
    public string ename;
    public string etype;
    [TextArea]
    public string edesc;
    public Sprite eImage;
    public WeaponData weapon; //neu trang bi nay la vu khi hoac khien
    public GameObject passive; //neu trang bi co kha nang dac biet
    public string passiveDesc; //mo ta kha nang dac biet
    public int strength;
    public int intelligence;
    public int dexerity;
    public int vitality;
    public int poise;
    [Header("Damage Resistance")]
    public int physicalRES;
    public int fireRES;
    public int iceRES;
    public int lightningRES;
    public int magicRES;
    [Header("Effect Resistance")]
    public int poison;
    public int burn;
    public int frozen;
    public int bleed;
    public int blind;
    [Header("Equip Required")]
    public int strlv;
    public int intlv;
    public int dexlv;
    public int vitlv;
    [Header("Equipment upgrade")]
    public int poiseup;
    public int strup;
    public int dexup;
    public int intup;
    public int vitup;
}
