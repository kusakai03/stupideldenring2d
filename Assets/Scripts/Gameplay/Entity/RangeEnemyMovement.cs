using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyMovement : BaseEnemyMovement
{
    private Vector2 direction;
    [SerializeField] private GameObject arrow;
    public override void Movement()
    {
        if (IfEnable() && target.GetTarget()){
            direction = target.GetTarget().transform.position - transform.position;
            transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
            AttackState();
        }
        if (state.State == EnemyState.EntityState.Attack && !target.GetTarget())
        SetDefault();
    }
    public void Shot(){
        att.ShotSomething(arrow, 1, "Physical", direction, 20);
    }
}
