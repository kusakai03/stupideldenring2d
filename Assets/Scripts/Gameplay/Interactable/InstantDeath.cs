using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            PlayerAttribute p = other.GetComponent<PlayerAttribute>();
            p.TakeDamage(p.currentHP, 0, "", null);
        }
    }
}
