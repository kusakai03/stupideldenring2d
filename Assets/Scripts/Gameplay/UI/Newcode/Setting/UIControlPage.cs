using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControlPage : MonoBehaviour
{
    [SerializeField] private Slider mobileButtonAlpha;
    [SerializeField] private CanvasGroup buttonTest;
    public void OnAlphaChange(){
        buttonTest.alpha = mobileButtonAlpha.value;
        GameSetting.Instance.mobileControlCapacity = mobileButtonAlpha.value;
    }
    private void OnEnable(){
        mobileButtonAlpha.value = GameSetting.Instance.mobileControlCapacity;
    }
    private void OnDisable(){
        GameSetting.Instance.SettingChanges();
    }
}
