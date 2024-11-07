using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class Arc0CS : MonoBehaviour
{
    [SerializeField] private Story[] lines;
    [SerializeField] private UIDialogBox uIDialogBox;
    [SerializeField] private GameObject enteryourname;
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private string confirmMessage;
    [SerializeField] private UIConfirmMessage confirmBox;
    [SerializeField] private int switchLineTime;
    private int endOfLinesCount;
    private void Start(){
        endOfLinesCount = 0;
        timeline.Play();
        Invoke(nameof(CutsceneState),1);
        uIDialogBox.onFinishedLines += OnFinishedLines;

    }
    private void OnDisable(){
        uIDialogBox.onFinishedLines -= OnFinishedLines;
    }

    private void OnFinishedLines(object sender, EventArgs e)
    {
        FinishLines();
    }
    private void FinishLines(){
        timeline.Resume();
        endOfLinesCount ++;
        Invoke(nameof(CutsceneState),switchLineTime);
    }
    private void CutsceneState(){
        timeline.Pause();
        switch(endOfLinesCount){
            case 0:
            uIDialogBox.SetValue(lines[0]);
            switchLineTime = 3;
            break;
            case 1:
            uIDialogBox.SetValue(lines[1]);
            switchLineTime = 4;
            break;
            case 2:
            uIDialogBox.SetValue(lines[2]);
            switchLineTime = 1;
            break;
            case 3:
            enteryourname.SetActive(true);
            switchLineTime = 3;
            break;
            case 4:
            uIDialogBox.SetValue(lines[3]);
            switchLineTime = 5;
            break;
            case 5:
            PlayerManager.Instance.Respawn();
            break;
        }
    }
    public void ConfirmName(){
        confirmBox.ShowConfirmMessage(confirmMessage, NameEntered);
    }
    private void NameEntered(){
        PlayerManager.Instance.SetPlayerName(enteryourname.GetComponentInChildren<TMP_InputField>().text);
        enteryourname.SetActive(false);
        FinishLines();
    }   
}
