using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestStatus{
    NotStarted,
    InProgress,
    Completed
}
[CreateAssetMenu(fileName = "Main Quest", menuName = "New Quest")]
public class GameQuest : ScriptableObject{
    public string questID;
    public string questName;
    [TextArea]
    public string questDescription;
    public List<ItemDrop> questRewards;
}
[Serializable]
public class QuestProgress{
    public GameQuest quest;
    public QuestStatus status = QuestStatus.NotStarted;
}
