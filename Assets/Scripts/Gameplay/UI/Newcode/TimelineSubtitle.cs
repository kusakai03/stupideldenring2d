using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimelineSubtitle : MonoBehaviour
{
    public List<TimelineDialogue> lines;
    [SerializeField] private TextMeshProUGUI texx;
    private int index;
    private string currentText;
    private void OnEnable(){
        index = 0;
    }

    public void SetValue(List<TimelineDialogue> lines){
        this.lines = lines;
    }

    public void PlaySubtitle(){
        index = 0;
        Invoke(nameof(NextLine),0);
    }
    private void ShowSubtitle(){
        texx.text = currentText;
    }
    private void HideSubtitle(){
        texx.text = "";
    }
    private void NextLine(){
        currentText = lines[index].line;
        ShowSubtitle();
        if (lines[index].lineDuration < lines[index].nextLineTime)
        Invoke(nameof(HideSubtitle),lines[index].lineDuration);
        if (index >= lines.Count-1){
            CancelInvoke(nameof(NextLine));
        }else{ 
            WaitForNextLine();
            index ++;
        }

    }
    private void WaitForNextLine(){
        Invoke(nameof(NextLine),lines[index].nextLineTime);
    }
}
[Serializable]
public class TimelineDialogue{
    [TextArea]
    public string line;
    public float lineDuration;
    public float nextLineTime;
}
