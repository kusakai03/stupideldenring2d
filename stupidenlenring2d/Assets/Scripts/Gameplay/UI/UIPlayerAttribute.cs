using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerAttribute : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI str;
    [SerializeField] private TextMeshProUGUI strbonus;
    [SerializeField] private TextMeshProUGUI intg;
    [SerializeField] private TextMeshProUGUI intgbonus;
    [SerializeField] private TextMeshProUGUI dex;
    [SerializeField] private TextMeshProUGUI dexbonus;
    [SerializeField] private TextMeshProUGUI vit;
    [SerializeField] private TextMeshProUGUI vitbonus;
    private void OnEnable(){
        UpdateAttribute();
        PlayerManager.Instance.onChangeEquipment += OnChangeEquipment;
    }

    private void OnChangeEquipment(object sender, EventArgs e)
    {
        UpdateAttribute();
    }

    public void UpdateAttribute(){
        PlayerAttribute a = PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>();
        str.text = a.lv.STR.ToString();
        strbonus.text = a.strength.ToString();
        intg.text = a.lv.INT.ToString();
        intgbonus.text = a.intelligence.ToString();
        dex.text = a.lv.DEX.ToString();
        dexbonus.text = a.dexerity.ToString();
        vit.text = a.lv.VIT.ToString();
        vitbonus.text = a.vitality.ToString();
    }
}
