using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private float damage;
    private float strength;
    private string damageType;
    public event EventHandler onBeingParried;
    public event EventHandler onHit;
    private bool isHit;
    private void OnTriggerStay2D(Collider2D other){
        if (other.tag.Equals("Player")){
            if (other.GetComponent<PlayerMoving>().state == PlayerMoving.playerState.Parry){
                onBeingParried?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (!isHit){
                isHit = true;
                other.gameObject.GetComponent<PlayerAttribute>().TakeDamage(damage,strength,damageType,transform);
                damage = 0;
                strength = 0;
                onHit?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public void SetObjectDamage(float damage, float strength, string damageType){
        this.damage = damage;
        this.strength = strength;
        this.damageType = damageType;
        isHit = false;
    }
}
