using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipmentSlotPage : MonoBehaviour
{
    private UIEquipmentList master;
    private EquipmentData equipment;
    [SerializeField] private Image eImage;
    [SerializeField] private GameObject equipState;
    private void OnEnable(){
        PlayerManager.Instance.onChangeEquipment += OnChangeEquipment;
    }
    private void OnDisable(){
        PlayerManager.Instance.onChangeEquipment -= OnChangeEquipment;
    }

    private void OnChangeEquipment(object sender, EventArgs e)
    {
        ShowValue();
    }

    public void SetValue(EquipmentData equipment, UIEquipmentList master){
        this.equipment = equipment;
        this.master = master;
        ShowValue();
    }
    private void ShowValue() {
    if (master.GetSelectedType().Contains("ring")) {
        var equippedRings = new List<EquipmentData> {
            PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>().ring1,
            PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>().ring2,
            PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>().ring3,
            PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>().ring4
        };
        equipState.SetActive(equippedRings.Contains(equipment));
    } else {
        equipState.SetActive(equipment == PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>().GetEquipmentSlotByType(master.GetSelectedType()));
    }
    eImage.sprite = PlayerManager.Instance.GetEquipmentByID(equipment?.eid).eImage;
}

    public void OnClick(){
        master.ShowEquipmentDetail(equipment);
    }
}
