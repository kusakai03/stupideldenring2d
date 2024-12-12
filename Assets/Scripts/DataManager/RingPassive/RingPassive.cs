using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPassive: MonoBehaviour
{
    public GameObject player;
    private void Awake(){
        player = PlayerManager.Instance.currentPlayer;
    }
    public virtual void Passive(){

    }
}
