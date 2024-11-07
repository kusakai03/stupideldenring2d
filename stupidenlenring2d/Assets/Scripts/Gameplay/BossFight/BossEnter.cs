using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
    private BoxCollider2D box;
    private void Awake(){
        box = GetComponent<BoxCollider2D>();
    }
    
}
