using System;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipmentSlot : MonoBehaviour
{
    [SerializeField] private GameObject equipmentTypeList;
    [SerializeField] private string slottype;
    [SerializeField] private Sprite blankicon;
    [SerializeField] private Image image;
    private EquipmentData equipment;
    public void OnClick(){
        equipmentTypeList.SetActive(true);
        equipmentTypeList.GetComponent<UIEquipmentList>().SetListType(slottype);
        equipmentTypeList.GetComponent<UIEquipmentList>().SetCurrentEquipping(equipment);
    }
    private void OnEnable(){
        PlayerManager.Instance.onChangeEquipment += OnChangeEquipment;
        FindESlot();
        ShowImage();
    }
    private void OnDisable(){
        PlayerManager.Instance.onChangeEquipment -= OnChangeEquipment;
    }
    private void FindESlot(){
        PlayerAttribute p = PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>();
        switch(slottype){
            case "weapon1":
            equipment = p.weapon1;
            break;
            case "weapon2":
            equipment = p.weapon2;
            break;
            case "helmet":
            equipment = p.helmet;
            break;
            case "armor":
            equipment = p.armor;
            break;
            case "glove":
            equipment = p.glove;
            break;
            case "leg":
            equipment = p.leg;
            break;
            case "ring1":
            equipment = p.ring1;
            break;
            case "ring2":
            equipment = p.ring2;
            break;
            case "ring3":
            equipment = p.ring3;
            break;
            case "ring4":
            equipment = p.ring4;
            break;
        }
    }
    private void OnChangeEquipment(object sender, EventArgs e)
    {
        FindESlot();
        ShowImage();
    }

    private void ShowImage(){
        if (PlayerManager.Instance.GetEquipmentByID(equipment?.eid)){
            image.sprite = PlayerManager.Instance.GetEquipmentByID(equipment.eid).eImage;
        }
        else image.sprite = blankicon;
    }

}
