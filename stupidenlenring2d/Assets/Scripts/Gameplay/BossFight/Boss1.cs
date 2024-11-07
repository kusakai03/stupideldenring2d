using UnityEngine;
using UnityEngine.UIElements;

public class Boss1 : BaseEnemyMovement
{
    [SerializeField] private GameObject summon;
    [SerializeField] private GameObject soul;
    private Vector2 direction;
    private bool isStarted;
    public void Enter(){
        Invoke(nameof(DoSomething),7);
    }
    private void DoSomething(){
        MoveState();
        Invoke(nameof(StartCombo), 2f);
    }
    public override void Movement()
    {
        if (target.GetTarget() && !isDead){
            direction = new Vector2(target.GetTarget().transform.position.x - transform.position.x, rb.velocity.y);
            transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
            if (!isStarted){
                Enter();
                isStarted = true;
            }
            if (state.State == EnemyState.EntityState.Moving){
                rb.velocity = direction.normalized * -moveSpeed;
        }}
    }
    private void Teleport(){
        transform.position = target.GetTarget().transform.position + new Vector3 (transform.localScale.x,0);
    }
    private void SoulAttack(){
        if (IfEnable())
        {
            SetDefault();
            AttackState();
        }
    }
    private void SummonAttack(){
        if (IfEnable()){
            SetDefault();        
            SummonState();
        }
    }
    private void StartCombo(){
        SetDefault();
        if (isDead){ DeadState();
        return;
        }
        if (!target.GetTarget()){
            SetDefault();
            return;
        }
        if (Vector2.Distance(transform.position, target.GetTarget().transform.position) > 10)
        {
            JumpState();
            Debug.Log("Gonna jump");
        }
        else{
            int ranNum = Random.Range(1, 6);
            switch (ranNum){
                case 1: 
                    DoSomething();
                    Debug.Log("Gonna move");
                break;
                case 2:
                    PunchState();
                    Debug.Log("Gonna attack");
                break;
                case 3:
                    SummonState();
                    Debug.Log("Gonna summon");
                break;
                case 4:
                    JumpState();
                    Debug.Log("Gonna jump");
                break;
                case 5:
                    SoulRangeAttack();
                    Debug.Log("Gonna shoot");
                break;
            }
        }
    }
    private void PunchState(){
        state.State = EnemyState.EntityState.Attack;
        an.SetTrigger("punch");
    }
    private void SummonState(){
        state.State = EnemyState.EntityState.Attack;
        an.SetTrigger("summon");
    }
    private void JumpState(){
        state.State = EnemyState.EntityState.Jump;
        an.SetTrigger("jump");
    }
    private void SoulRangeAttack(){
        state.State = EnemyState.EntityState.Attack;
        an.SetTrigger("range");
    }
    private void SummonDamage(){
        att.SetAttackDamage(2.3f, "Magic");
    }
    private void SoulDamage(){
        att.SetAttackDamage(1.5f, "Magic");
    }
    private void SoulInstance(){
        att.ShotSomething(soul, 3f, "Magic", direction, 1);
    }
    private void SummonInstance(){
        Instantiate(summon, GetComponentInChildren<Hitbox>().transform.position,Quaternion.identity);
    }
}
