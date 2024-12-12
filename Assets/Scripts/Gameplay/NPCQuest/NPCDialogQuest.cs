using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCDialogQuest : MonoBehaviour
{
    public List<QuestDialog> questDialogs;
    private Story currentLine;
    [SerializeField] private Story defaultLine;
    private DialogueLines speak;
    private void Awake(){
        speak = GetComponent<DialogueLines>();
    }
    private void Start(){
        GameSetting.Instance.onFinishedLines += OnFinishedLines;
        CheckingDialogue();
    }
    public Story GetDefaultLine(){
        return defaultLine;
    }
    private void OnFinishedLines(object sender, EventArgs e)
    {
        if (GameSetting.Instance.lastStoryLine == currentLine)
        {
            var dialogue = questDialogs.FirstOrDefault(q => q.line == currentLine);
            if (dialogue != null){
            if (dialogue.isFinalQuestline)
                QuestManager.Instance.CompleteQuest(dialogue.questID);
            dialogue.isDone = true;
            }

        }
        CheckingDialogue();
    }

    public void CheckingDialogue(){
        var dialogue = questDialogs.FirstOrDefault(q =>
        QuestManager.Instance.GetQuestByID(q.questID)?.status == QuestStatus.InProgress && !q.isDone);
        if (dialogue != null){
            currentLine = dialogue.line;
        }
        else currentLine = defaultLine;
        UpdateDialogueLine();
    }
    private void UpdateDialogueLine(){
        speak.story = currentLine;
    }
}
[Serializable]
public class QuestDialog{
    public string questID;
    public Story line;
    public bool isFinalQuestline;
    public bool isDone;
}