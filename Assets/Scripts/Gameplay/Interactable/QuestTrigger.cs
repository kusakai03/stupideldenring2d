using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private string questid;
    [SerializeField] private bool openState;
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            if (openState){
                QuestManager.Instance.CompleteQuest(questid);
            }
            else QuestManager.Instance.UpdateQuestStatus(questid, QuestStatus.InProgress);
        }
    }
}
