using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplay : MonoBehaviour
{
    public static UIGameplay Instance { get; private set; }
    [SerializeField] private UITrading tradingUI;
    [SerializeField] private UIForgeEquipment forgeUI;
    [SerializeField] private CanvasGroup mobileButton;
    private void Awake(){
        Instance = this;
    }
    private void OnEnable(){
        GameSetting.Instance.onSettingChanges += OnSettingChanges;
        ChangeMobileButtonAlpha();
    }
    private void OnDisable(){
        GameSetting.Instance.onSettingChanges -= OnSettingChanges;
    }

    private void OnSettingChanges(object sender, EventArgs e)
    {
        ChangeMobileButtonAlpha();

    }

    public void ShowTradingUI(List<ItemSell> items){
        tradingUI.gameObject.SetActive(true);
        tradingUI.SetItemToSell(items);
        tradingUI.UpdateItemPage();
    }
    public void ShowForgingUI(List<EquipmentForge> forges){
        forgeUI.gameObject.SetActive(true);
        forgeUI.SetEquipmentList(forges);
    }
    public void ShowSettingUI(){
        GameSetting.Instance.OpenSettingUI();
    }
    private void ChangeMobileButtonAlpha(){
        mobileButton.alpha = GameSetting.Instance.mobileControlCapacity;
    }

}
