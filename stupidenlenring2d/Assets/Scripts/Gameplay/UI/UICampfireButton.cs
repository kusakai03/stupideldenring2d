using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICampfireButton : MonoBehaviour
{
    private Campfire campfire;
    private GameObject UIRest;
    private void Awake(){
        UIRest = FindObjectOfType<UISavegame>().gameObject;
    }
    public void SetValue(Campfire campfire){
        gameObject.SetActive(true);
        this.campfire = campfire;
    }
    private void Update(){
        if (campfire){
            if (!campfire.player){
                campfire = null;
                gameObject.SetActive(false);
            }
        }
    }
    public void OnClick(){
        UIRest.SetActive(true);
        UIRest.GetComponent<UISavegame>().StartAnimate();
    }
}
