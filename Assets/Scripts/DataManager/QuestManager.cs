using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set;}
    public List<QuestProgress> playerQuest;
    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    public void UpdateQuestStatus(string questID, QuestStatus status){
        GetQuestByID(questID).status = status;
    }
    public void CompleteQuest(string questID){
        UpdateQuestStatus(questID, QuestStatus.Completed);
        ItemManager.ins.RewardItems(GetQuestByID(questID).quest.questRewards);
    }
    public QuestProgress GetQuestByID(string questID){
        return playerQuest.Find(a => a.quest.questID == questID);
    }
}
