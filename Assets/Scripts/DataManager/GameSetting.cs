using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance { get; private set;}
    public event EventHandler onFinishedLines;
    public event EventHandler onSettingChanges;
    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;
    public Vector2 joystickDir;
    public float mobileControlCapacity;
    [SerializeField] private UIConfirmMessage confirmMessage;
    [SerializeField] private UISavegame restingScreen;
    [SerializeField] private UIChoosingReward choosingReward;
    [SerializeField] private GameObject floatingDamage;
    [SerializeField] private GameObject gameSettingUI;
    public Story lastStoryLine{get; private set;} 
    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    private void Start(){
        AudioManager.Instance.UpdateVolume();
    }
    public void StartResting(){
        restingScreen.gameObject.SetActive(true);
        restingScreen.StartAnimate();
    }
    public void OnVolumeChange(float master, float bgm, float sfx){
        masterVolume = master;
        bgmVolume = bgm;
        sfxVolume = sfx;
        AudioManager.Instance?.UpdateVolume();
    }
    public void ShowConfirmMessage(string message, Action action){
        confirmMessage.ShowConfirmMessage(message, action);
    }
    public void ShowWarningMessage(string message){
        
    }
    public void OpenSettingUI(){
        gameSettingUI.SetActive(true);
    }
    public void SettingChanges(){
        onSettingChanges?.Invoke(this, EventArgs.Empty);
    }
    public void SelectReward(List<ItemDrop> items){
        choosingReward.ShowRewardChoosingBox(items);
    }
    public void ShowFloatingDamage(int damage, string damageType, Vector2 position){
        TextMeshPro fd = Instantiate(floatingDamage, position, Quaternion.identity).GetComponent<TextMeshPro>();
        fd.text = damage.ToString();
        if (damageType == "Physical"){
            fd.color = Color.white;
        }
        if (damageType == "Fire"){
            fd.color = Color.red;
        }
        if (damageType == "Ice"){
            fd.color = Color.blue;
        }
        if (damageType == "Lightning"){
            fd.color = Color.yellow;
        }
        if (damageType == "Magic"){
            fd.color = Color.magenta;
        }
        if (damageType == ""){
            fd.color = Color.gray;
        }
    }
    public void OnFinishedLines(Story line){
        lastStoryLine = line;
        onFinishedLines?.Invoke(this, EventArgs.Empty);
    }
}
