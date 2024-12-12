using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHit : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float strength;
    private void OnCollisionStay2D(Collision2D col){
        if (col.gameObject.tag == "Player"){
            col.gameObject.GetComponent<PlayerAttribute>().TakeDamage(damage, strength, "Physical", transform);
        }
    }
}
