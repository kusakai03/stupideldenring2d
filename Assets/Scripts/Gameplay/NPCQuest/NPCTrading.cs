using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrading : MonoBehaviour
{
    [SerializeField] private List<ItemSell> sells;
    private void Start(){
        GameSetting.Instance.onFinishedLines += OnFinishedLines;
    }
    private void OnDisable(){
        GameSetting.Instance.onFinishedLines -= OnFinishedLines;
    }

    private void OnFinishedLines(object sender, EventArgs e)
    {
        if (GameSetting.Instance.lastStoryLine == GetComponent<NPCDialogQuest>().GetDefaultLine()){
            UIGameplay.Instance.ShowTradingUI(sells);
        }
    }
}
