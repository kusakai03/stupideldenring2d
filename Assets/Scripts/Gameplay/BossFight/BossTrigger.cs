using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private float camsize;
    [SerializeField] private GameObject[] bossEnters;
    private bool trigger;
    [SerializeField] private EntityAttribute boss;
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private GameObject cameraPoint;
    private GameObject player;
    [SerializeField] private bool hasCutscene;
    [SerializeField] private PlayableDirector timeline;
    void Start()
    {
        boss.onOutOfHealth += OnOutOfHealth;
        timeline.stopped += OnTimelineFinish;
    }

    private void OnTimelineFinish(PlayableDirector director)
    {
        SetCamera();
        boss.gameObject.SetActive(true);
    }

    private void OnOutOfHealth(object sender, EventArgs e)
    {
        Invoke(nameof(EndGame), 12);
    }
    private void EndGame(){
        OpenTheDoor();
        CameraDefault();
        bossHealthBar.SetActive(false);
    }
    private void OnTriggerEnter2D (Collider2D other){
        if (other.tag == "Player"){
            if (!trigger){
                trigger = true;
                CloseDoor();
                player = other.gameObject;
                player.GetComponent<PlayerMoving>().AnimationEnd();
                if (hasCutscene)
                    timeline.Play();
                else{
                    SetCamera();
                }
            }
        }
    }
    private void SetCamera(){
        CameraFollow.Instance.SetTarget(cameraPoint);
        CameraFollow.Instance.SetCamSize(camsize);
        bossHealthBar.GetComponent<BossHealthBar>().SetValue(boss);
        bossHealthBar.SetActive(true);
    }
    private void CameraDefault(){
        CameraFollow.Instance.SetTarget(player);
        CameraFollow.Instance.SetCamSize(6);
    }
    private void CloseDoor(){
        foreach(var door in bossEnters){
            door.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
    private void OpenTheDoor(){
        foreach(var door in bossEnters){
            door.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
