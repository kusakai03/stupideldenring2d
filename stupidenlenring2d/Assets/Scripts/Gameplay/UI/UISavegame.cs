using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UISavegame : MonoBehaviour
{
    [SerializeField] private GameObject UIconfirmMessage;
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private GameObject restOptionUI;
    [SerializeField] private GameObject skillPageUI;
    private void SaveLoad_Instance_OnRequestTimeout(object sender, EventArgs e)
    {
        UIconfirmMessage.SetActive(true);
        UIconfirmMessage.GetComponent<UIConfirmMessage>().ShowConfirmMessage("Kết nối mạng thất bại, bạn muốn thử lại?", StartSaving);
    }

    private void PlayerManager_Instance_OnSpawnSuccess(object sender, EventArgs e)
    {
        timeline.Resume();
        PlayerManager.Instance.onSpawnSuccess -= PlayerManager_Instance_OnSpawnSuccess;
    }

    public void StartSaving(){
        SaveLoad.Instance.onSaveSucceed += SaveLoad_Instance_OnSaveSucceed;
        PlayerManager.Instance.onSpawnSuccess += PlayerManager_Instance_OnSpawnSuccess;
        SaveLoad.Instance.onRequestTimeout += SaveLoad_Instance_OnRequestTimeout;
        PlayerManager.Instance.SaveData();
        timeline.Pause();
    }
    public void StartAnimate(){
        restOptionUI.SetActive(false);
        timeline.Play();
        Invoke(nameof(StartSaving), 1);
    }
    private void SaveLoad_Instance_OnSaveSucceed(object sender, EventArgs e)
    {
        restOptionUI.SetActive(true);
    }
    public void ExitResting(){
        restOptionUI.SetActive(false);
        SceneLoader.Instance.LoadScene("");
        SaveLoad.Instance.onSaveSucceed -= SaveLoad_Instance_OnSaveSucceed;
        SaveLoad.Instance.onRequestTimeout -= SaveLoad_Instance_OnRequestTimeout;
    }
    public void OpenSkillPage(){
        skillPageUI.SetActive(true);
        skillPageUI.GetComponent<UISkillEquip>().UpdateSkillList();
    }
}
