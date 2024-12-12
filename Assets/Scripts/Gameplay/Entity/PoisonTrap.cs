using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrap : MonoBehaviour
{
    [SerializeField] private string debuffType;
    [SerializeField] private int strength;
    public List<GameObject> players;
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            if (!players.Contains(other.gameObject))
                players.Add(other.gameObject);
            InvokeRepeating(nameof(Eff),0,0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if (players.Contains(other.gameObject)){
            players.Remove(other.gameObject);
        }
    }
    private void Eff(){
        if (players.Count >= 1){
            foreach (var player in players)
            player.GetComponent<PlayerAttribute>().ApplyStatusEffect(debuffType, strength);  
        }
        else CancelInvoke(nameof(Eff));
    }
}
