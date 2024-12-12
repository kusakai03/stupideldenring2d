using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItem : MonoBehaviour
{
    public GameObject player;
    private void Start(){
        player = PlayerManager.Instance.currentPlayer;
        OnUsed();
    }
    public virtual void OnUsed(){

    }
}
