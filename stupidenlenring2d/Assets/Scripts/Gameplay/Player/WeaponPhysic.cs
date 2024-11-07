using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponPhysic : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float strength;
    private PlayerMoving master;
    public List<GameObject> hitObjects;
    public event EventHandler onWeaponHit;
    public void SetDamage(float damage, float strength){
        this.damage = damage;
        this.strength = strength;
        hitObjects.Clear();
    }
    public void SetMaster(PlayerMoving master){
        this.master = master;
    }
    public void OnTriggerEnter2D(Collider2D coll){
        if (coll.tag == "Entity"){
            if (!hitObjects.Contains(coll.gameObject) && coll.GetComponent<EnemyState>().State != EnemyState.EntityState.Dead && master.state != PlayerMoving.playerState.Parry && master.state != PlayerMoving.playerState.Block && master.state != PlayerMoving.playerState.Evade){
                coll.GetComponent<EntityAttribute>().TakeDamage(damage, strength, "Physical", transform);   
                master.GetComponent<PlayerAttribute>().LastDamageDeal(damage);
                hitObjects.Add(coll.gameObject);
                onWeaponHit?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
