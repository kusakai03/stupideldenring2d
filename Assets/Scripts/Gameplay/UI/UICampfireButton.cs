using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICampfireButton : MonoBehaviour
{
    private Campfire campfire;
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
        GameSetting.Instance.StartResting();
    }
}
