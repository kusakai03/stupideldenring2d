using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private EntityAttribute boss;
    private Slider slider;
    private TextMeshProUGUI bossName;
    private void Awake(){
        slider = GetComponentInChildren<Slider>();
        bossName = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable(){
        if(boss != null){
            slider.maxValue = boss.GetHp();
            bossName.text = boss.bossName;
        }
    }
    private void Update(){
        if(boss != null){
            slider.value = boss.GetCurrentHP();
        }
    }
    public void SetValue(EntityAttribute boss){
        this.boss = boss;
    }
}
