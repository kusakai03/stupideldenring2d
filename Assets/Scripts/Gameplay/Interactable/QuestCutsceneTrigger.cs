using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class QuestCutsceneTrigger : MonoBehaviour
{
    [SerializeField] private string questid;
    [SerializeField] private bool canCompleteQuest;
    [SerializeField] private PlayableDirector playable;
    [SerializeField] private TimelineSubtitle subtitle;
    [SerializeField] private List<TimelineDialogue> lines;
    private void Awake(){
        if (QuestManager.Instance.GetQuestByID(questid).status != QuestStatus.InProgress)
        {
            gameObject.SetActive(false);
        }
    }
    private void Start(){
        playable.stopped += OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        PlayerManager.Instance.currentPlayer.gameObject.SetActive(true);
        CameraFollow.Instance.SetTarget(PlayerManager.Instance.currentPlayer);
        CameraFollow.Instance.SetCamSize(6);
        if (QuestManager.Instance.GetQuestByID(questid).status == QuestStatus.InProgress && canCompleteQuest){
            QuestManager.Instance.CompleteQuest(questid);
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            playable.Play();
            PlayerManager.Instance.currentPlayer.gameObject.SetActive(false);
            subtitle.SetValue(lines);
            subtitle.PlaySubtitle();
        }
    }
}
