using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFindPlayer : MonoBehaviour
{
    private GameObject target;
    private void OnTriggerStay2D(Collider2D col){
        if (col.tag.Equals("Player")){
            if (col.GetComponent<PlayerMoving>().state != PlayerMoving.playerState.Dead)
            target = col.gameObject;
        }
    }
    private void Update(){
        if (target && target.GetComponent<PlayerMoving>().state == PlayerMoving.playerState.Dead)
        SpareTheTarget();
    }
    public GameObject GetTarget(){
        return target;
    }
    public void SpareTheTarget(){
        target = null;
    }
}
