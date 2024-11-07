using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLines : MonoBehaviour
{
    public Story story;
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            player = other.gameObject;
            MobileButton.Instance.ShowInterractButton(this);
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Player" && player == other.gameObject){
            player = null;
        }
    }
}
