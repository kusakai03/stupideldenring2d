using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplayBar : MonoBehaviour
{
    public static UIGameplayBar Instance { get; private set;}
    [SerializeField] private Slider hbar;
    [SerializeField] private Slider mbar;
    [SerializeField] private Slider sbar;
    public RectTransform statusEffectBar;
    private void Awake(){
        if (Instance == null){
            Instance = this;
        }
    }
    public void SetMaxValue(float value1 = 1, float value2 = 1, float value3 = 1){
        hbar.maxValue = value1;
        mbar.maxValue = value2;
        sbar.maxValue = value3;
    }
    public void SetCurrentValue(float value1, float value2, float value3){
        hbar.value = value1;
        mbar.value = value2;
        sbar.value = value3;   
    }
}
