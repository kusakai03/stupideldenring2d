using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyMovement : MonoBehaviour
{
    public EnemyState state { get; private set; }
    public EnemyFindPlayer target { get; private set; }
    public Animator an { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityAttribute att { get; private set; }
    public bool isDead { get; private set; }
    public float moveSpeed;
    private void Awake(){
        state = GetComponent<EnemyState>();
        target = GetComponentInChildren<EnemyFindPlayer>();
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        att = GetComponent<EntityAttribute>();
    }
    private void Start(){
        att.onGettingHit += OnGettingHit;
        att.onOutOfPoise += OnOutOfPoise;
        att.onOutOfHealth += OnOutOfHealth;
    }
    private void OnDisable(){
        att.onGettingHit -= OnGettingHit;
        att.onOutOfPoise -= OnOutOfPoise;
        att.onOutOfHealth -= OnOutOfHealth;
    }
    private void OnOutOfHealth(object sender, EventArgs e)
    {
        SetDefault();
        DeadState();
        CancelInvoke(nameof(SetDefault));
    }

    private void OnOutOfPoise(object sender, EventArgs e)
    {
        SetDefault();
        DazeState();
        Invoke(nameof(SetDefault), 2);
    }

    private void OnGettingHit(object sender, EventArgs e)
    {
        if (state.State != EnemyState.EntityState.Daze){
            SetDefault();
            HitState();
            Invoke(nameof(SetDefault), 0.1f);
        }
    }

    private void FixedUpdate(){
        Movement();
    }

    public virtual void Movement(){

    }

    public bool FindTarget(){
        return target.GetTarget();
    }
    public void SetDefault(){
        if (state.State == EnemyState.EntityState.Daze) att.WakeFromFaint();
        state.State = EnemyState.EntityState.Idle;
        an.SetBool("moving", false);
        an.SetBool("attack", false);
        an.SetBool("hit", false);
        CancelInvoke(nameof(SetDefault));
    }
    public void MoveState(){
        state.State = EnemyState.EntityState.Moving;
        an.SetBool("moving", true);
    }
    public void AttackState(){
        state.State = EnemyState.EntityState.Attack;
        an.SetBool("attack", true);
    }
    private void HitState(){
        state.State = EnemyState.EntityState.Hit;
        an.SetBool("hit", true);
    }
    private void DazeState(){
        state.State = EnemyState.EntityState.Daze;
        an.SetBool("hit", true);
    }
    public void DeadState(){
        if (!isDead){
            state.State = EnemyState.EntityState.Dead;
            an.SetTrigger("dead");
            isDead = true;
            Invoke(nameof(Corpse),5);
        }
    }
    public void Corpse(){
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }
    private void JumpState(){
        state.State = EnemyState.EntityState.Jump;
        an.SetTrigger("jump");
    }
    public void RepeatTheAttack(){
        att.SetAttackDamage(1, "Physical");
    }
    public bool IfEnable(){
        return state.State != EnemyState.EntityState.Daze && state.State != EnemyState.EntityState.Dead && state.State != EnemyState.EntityState.Hit;
    }
}
