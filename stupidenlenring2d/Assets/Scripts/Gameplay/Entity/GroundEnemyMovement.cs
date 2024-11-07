using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : BaseEnemyMovement
{
    public override void Movement()
    {
        if (state.State != EnemyState.EntityState.Daze || state.State != EnemyState.EntityState.Dead || state.State != EnemyState.EntityState.Hit)
        switch (state.State){
            case EnemyState.EntityState.Idle:
                if (FindTarget()){
                    MoveState();
                }
            break;
            case EnemyState.EntityState.Moving:
                if (FindTarget()){
                    Vector2 dir = new Vector2(target.GetTarget().transform.position.x - transform.position.x, rb.velocity.y);
                    rb.velocity = dir.normalized * moveSpeed;
                    if (Mathf.Abs(dir.x) > 0.1f) {
                        transform.localScale = new Vector2(Mathf.Sign(dir.x), 1);
                    }
                    if (Vector2.Distance(target.GetTarget().transform.position, transform.position) > 40){
                        target.SpareTheTarget();
                        SetDefault();
                    }
                }
            break;
            case EnemyState.EntityState.Attack:
                if (!FindTarget()){
                    SetDefault();
                }
            break;
        }
    }
    private void OnCollisionStay2D(Collision2D col){
        if (col.gameObject.tag == "Player"){
            if (IfEnable()){
                SetDefault();
                AttackState();
            }
        }
    }
}
