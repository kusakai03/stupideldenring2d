using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start(){
        GetComponent<Hitbox>().onHit += OnHit;
    }

    private void OnHit(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
