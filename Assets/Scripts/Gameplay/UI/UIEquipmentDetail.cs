using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipmentDetail : MonoBehaviour
{
    [SerializeField] private UIEquipmentList list;
    //Hiện tất cả thông tin của trang bị được chọn
    private EquipmentData selectedEquipment; 
    [SerializeField] private TextMeshProUGUI eName;
    [SerializeField] private Image eSprite;
    [SerializeField] private TextMeshProUGUI eType;
    [SerializeField] private TextMeshProUGUI eDesc;
    [SerializeField] private TextMeshProUGUI ePassive;
    [SerializeField] private TextMeshProUGUI eLv;
    [SerializeField] private TextMeshProUGUI str;
    [SerializeField] private TextMeshProUGUI intg;
    [SerializeField] private TextMeshProUGUI dex;
    [SerializeField] private TextMeshProUGUI vit;
    [SerializeField] private TextMeshProUGUI poi;
    [Header ("Armor resistance")]
    [SerializeField] private TextMeshProUGUI physres;
    [SerializeField] private TextMeshProUGUI fireres;
    [SerializeField] private TextMeshProUGUI iceres;
    [SerializeField] private TextMeshProUGUI lninres;
    [SerializeField] private TextMeshProUGUI magicres;
    [SerializeField] private TextMeshProUGUI poisonres;
    [SerializeField] private TextMeshProUGUI bleedres;
    [SerializeField] private TextMeshProUGUI frozenres;
    [SerializeField] private TextMeshProUGUI burnres;
    [SerializeField] private TextMeshProUGUI blindres;
    [Header("Weapon block state")]
    [SerializeField] private TextMeshProUGUI physblock;
    [SerializeField] private TextMeshProUGUI fireblock;
    [SerializeField] private TextMeshProUGUI iceblock;
    [SerializeField] private TextMeshProUGUI lninblock;
    [SerializeField] private TextMeshProUGUI magicblock;
    [Header ("Equip requirement")]
    [SerializeField] private TextMeshProUGUI strlv;
    [SerializeField] private TextMeshProUGUI intglv;
    [SerializeField] private TextMeshProUGUI dexlv;
    [SerializeField] private TextMeshProUGUI vitlv;
    [Header ("Object")]
    [SerializeField] private GameObject attributetext;
    [SerializeField] private GameObject weaponStat;
    [SerializeField] private GameObject armorStat;
    [SerializeField] private GameObject info;
    [SerializeField] private TextMeshProUGUI equipState;
    private bool switchview;
    private bool isEquipped;
    public void SetValue(EquipmentData data){
        selectedEquipment = data;
        SetText();
    }
    private void SetText(){
        if (selectedEquipment != null){
            Equipment s = PlayerManager.Instance.GetEquipmentByID(selectedEquipment.eid);
            eName.text = s.ename;
            eSprite.sprite = s.eImage;
            eDesc.text = s.edesc;
            eType.text = s.etype;
            ePassive.text = s.passiveDesc;
            eLv.text = selectedEquipment.level.ToString();

            str.text = "STR "+s.strength.ToString();
            dex.text = "DEX "+s.dexerity.ToString();
            intg.text = "INT "+s.intelligence.ToString();
            vit.text = "VIT "+s.vitality.ToString();
            poi.text = "POI "+s.poise.ToString();

            strlv.text = "STR "+s.strlv.ToString();
            intglv.text = "INT "+s.intlv.ToString();
            dexlv.text = "DEX "+s.dexlv.ToString();
            vitlv.text = "VIT "+s.vitlv.ToString();
            SetEquipmentTypeStat();
            EquipButtonState();
        }
    }
    private void SetEquipmentTypeStat(){
        Equipment s = PlayerManager.Instance.GetEquipmentByID(selectedEquipment.eid);
        if (selectedEquipment.eType == "weapon"){
            armorStat.SetActive(false);
            weaponStat.SetActive(true);
            physblock.text = "PHYSICAL "+s.weapon.physicalRes.ToString();
            fireblock.text = "FIRE "+s.weapon.fireRes.ToString();
            iceblock.text = "ICE "+s.weapon.iceRes.ToString();
            lninblock.text = "LIGHTNING "+s.weapon.lightningRes.ToString();
            magicblock.text = "MAGIC "+s.weapon.magicRes.ToString();
            ePassive.text = s.weapon.weaponSkill?.skillDescription;
        }
        else{
            armorStat.SetActive(true); 
            weaponStat.SetActive(false);
            physres.text = "PHYSICAL "+s.physicalRES.ToString();
            fireres.text = "FIRE "+s.fireRES.ToString();
            iceres.text = "ICE "+s.iceRES.ToString();
            lninres.text =  "LIGHTNING "+s.lightningRES.ToString();
            magicres.text =  "MAGIC "+s.magicRES.ToString();
            poisonres.text =  "POISON "+s.poison.ToString();
            burnres.text =  "BURN "+s.burn.ToString();
            frozenres.text =  "FROZEN "+s.frozen.ToString();
            bleedres.text =  "BLEED "+s.bleed.ToString();
            blindres.text =  "BLIND "+s.blind.ToString();
        }
    }
    public void SwitchView(){
        switchview = !switchview;
        attributetext.SetActive(!switchview);
        info.SetActive(switchview);
    }
    public void EquipButton(){
        if (isEquipped){
            PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>().CancelEquip(list.GetSelectedType());
        }
        else PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>().Equipment(selectedEquipment, list.GetSelectedType());
        SetText();
        PlayerManager.Instance.ChangeEquipment();
    }
    public void EquipButtonState(){
        equipState.text = selectedEquipment != PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>().GetEquipmentSlotByType(list.GetSelectedType())? "Trang bị": "Hủy trang bị";
        isEquipped = selectedEquipment == PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>().GetEquipmentSlotByType(list.GetSelectedType())? true: false;
    }
}
