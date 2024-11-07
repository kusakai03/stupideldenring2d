using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogBox : MonoBehaviour
{
    private int currentPage;
    private Story story;
    [SerializeField] private TextMeshProUGUI line;
    public event EventHandler onFinishedLines;
    private bool skipable;
    [SerializeField] private float fillTime = 0.1f;
    private string currentLine;
    private int currentChar;
    public void SetValue(Story story){
        this.story = story;
        gameObject.SetActive(true);
        StartDialogue();
    }
    private void StartDialogue(){
        currentPage = 0;
        ShowPage();
    }
    private void ShowPage(){
        if (currentPage < story.storyLines.Count){
            skipable = false;
            currentChar = 0;
            this.line.text = "";
            string line = story.storyLines[currentPage];
            string newLine = line.Replace("!playername", PlayerManager.Instance.playerName);
            currentLine = newLine;
            InvokeRepeating(nameof(ShowText),0,fillTime);
        }
        else{
            onFinishedLines?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }
    }
    private void ShowText(){
        if (currentChar < currentLine.Length){
            line.text += currentLine[currentChar];
        }
        else{
            CancelInvoke(nameof(ShowText));
            skipable = true;
        }
        currentChar ++;
    }
    public void OnClick(){
        if (skipable){
            currentPage ++;
            ShowPage();
        }
    }
}
