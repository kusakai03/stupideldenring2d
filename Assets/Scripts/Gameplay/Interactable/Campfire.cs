using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            player = other.gameObject;
            MobileButton.Instance.ShowCampfireButton(this);
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Player" && player == other.gameObject){
            player = null;
        }
    }
}
