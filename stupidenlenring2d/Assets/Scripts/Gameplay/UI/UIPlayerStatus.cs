using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI mp;
    [SerializeField] private TextMeshProUGUI sp;
    [SerializeField] private TextMeshProUGUI patk;
    [SerializeField] private TextMeshProUGUI matk;
    [SerializeField] private TextMeshProUGUI poi;
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
    private void OnEnable(){
        UpdateStatus();
    }
    private void UpdateStatus(){
        PlayerAttribute a = PlayerManager.Instance.currentPlayer.GetComponent<PlayerAttribute>();
        hp.text =  (int)a.currentHP + "/" + a.finalHP;
        mp.text =  (int)a.currentMP + "/" + a.finalMP;
        sp.text = (int)a.currentStamina + "/" + a.finalStamina;
        patk.text = a.finalATK.ToString();
        matk.text = a.finalMAG.ToString();
        poi.text = a.finalPoise.ToString();
        physres.text = a.finalPhysicalRes.ToString();
        fireres.text = a.finalFireRes.ToString();
        iceres.text = a.finalIceRes.ToString();
        lninres.text = a.finalLightningRes.ToString();
        magicres.text = a.finalMagicRes.ToString();
        poisonres.text = a.poison.ToString();
        burnres.text = a.burn.ToString();
        frozenres.text = a.frozen.ToString();
        bleedres.text = a.bleed.ToString();
        blindres.text = a.blind.ToString();
    }
}
