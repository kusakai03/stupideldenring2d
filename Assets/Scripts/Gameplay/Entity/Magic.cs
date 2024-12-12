using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private Animator an;
    private Rigidbody2D rb;
    private void Awake(){
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start(){
        GetComponent<Hitbox>().onHit += OnHit;
    }

    private void OnHit(object sender, EventArgs e)
    {
        an.SetTrigger("xplo");
        transform.localScale = new Vector2(6,6);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void End(){
        Destroy(gameObject);
    }
}
