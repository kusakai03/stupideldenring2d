using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public event EventHandler onNormalAttack;
    public event EventHandler onChargeAttack;
    public event EventHandler onDashing;
    public event EventHandler onBlocking;
    public event EventHandler onAttackFinished;
    public event EventHandler onUsingSkill;
    public event EventHandler onFinishedSkill;
    private Rigidbody2D rb;
    private Animator an;
    private float defaultMovespeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private int normalAtkCombo;
    [SerializeField] private int maxComboAtk;
    private float maxChargeTime = 2;
    private float chargeTime = 0;
    private float movey;
    private bool isCharging;
    private bool canAttack = true;
    private bool immortal = false;
    public bool isLadder { get; private set; }
    private float flyingTime;
    private bool isGround;
    private bool isDead;
    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isMovingUp;
    private bool isMovingDown;
    private bool isJumping;
    public playerState state;
    private Vector2 movement;
    private PlayerAttribute attribute;
    public string skillDoing { get; private set; }
    [SerializeField] private GameObject gameover;
    public enum playerState{
        Idle, //Đứng im không làm gì
        Moving, //Di chuyển
        Attack, //Đánh thường
        StrongAttack, //Đánh mạnh
        Jumping,
        Evade, //Né,
        Parry, //Phản đòn
        Block, //Đỡ
        Daze, //Choáng
        Dead //Tử
    }
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        attribute = GetComponent<PlayerAttribute>();
        an = GetComponent<Animator>();  
    }
    private void OnEnable(){
        isDead = false;
        CameraFollow.Instance.SetTarget(gameObject);
        MobileButton.onJumpButton += OnJumpButton;
        attribute.onOutOfPoise += OnOutOfPoise;
        attribute.onGettingHit += OnGettingHit;
        attribute.onOutOfHealth += OnOutOfHealth;
    }
    
    private void Start(){
        moveSpeed = attribute.lv.GetMoveSpeed(attribute.dexerity);
        defaultMovespeed = moveSpeed;
    }
    private void OnOutOfHealth(object sender, EventArgs e)
    {
        state = playerState.Dead;
        if (!isDead){
            isDead = true;
            Instantiate(gameover, GameObject.FindGameObjectWithTag("Canvas").transform);
        }
        CancelInvoke(nameof(AnimationEnd));
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnGettingHit(object sender, EventArgs e)
    {
        immortal = true;
        Invoke(nameof(CanGetHit), 0.1f);
    }
    private void CanGetHit(){
        immortal = false;
    }

    private void OnOutOfPoise(object sender, EventArgs e)
    {
        AnimationEnd();
        state = playerState.Daze;
        an.SetBool("daze", true);
        if (state != playerState.Dead)
        Invoke(nameof(AnimationEnd),3);
    }
    private void OnDisable(){
        MobileButton.onJumpButton -= OnJumpButton;
        attribute.onOutOfPoise -= OnOutOfPoise;
        attribute.onGettingHit -= OnGettingHit;
    }
    private void Update(){
        if (isDead) state = playerState.Dead;
        if (defaultMovespeed != attribute.lv.GetMoveSpeed(attribute.dexerity))
        defaultMovespeed = attribute.lv.GetMoveSpeed(attribute.dexerity);
        CheckFallDamage();
    }
    private void FixedUpdate(){
        if (isPlayerFine()) {
            if (state != playerState.Evade){
                if (isMovingLeft){
                movement = Vector2.left;
                transform.localScale = new Vector2(-0.6f, transform.localScale.y);
                }
                else if (isMovingRight){
                movement = Vector2.right;
                transform.localScale = new Vector2(0.6f, transform.localScale.y);
                }
                else if (isMovingUp)
                if (isLadder)
                movement = Vector2.up;
                else if (isMovingDown)
                if (isLadder)
                movement = Vector2.down;
            }
            else{
                Vector2 dashDirection = new Vector2(transform.localScale.x, 0).normalized;
                rb.velocity = dashDirection * 10;
            }
        if (movement != Vector2.zero)
            if (!isLadder)
            rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
            else rb.velocity = movement * moveSpeed;
        }
    }
    public void MoveDirection(string dir){
        switch (dir){
            case "w":
            isMovingUp = true;
            break;
            case "a":
            isMovingLeft = true;
            break;
            case "s":
            isMovingDown = true;
            break;
            case "d":
            isMovingRight = true;
            break;
        }
        state = playerState.Moving;
    }
    public void StopDirection(string dir){
        switch (dir){
            case "w":
            isMovingUp = false;
            break;
            case "a":
            isMovingLeft = false;
            break;
            case "s":
            isMovingDown = false;
            break;
            case "d":
            isMovingRight = false;
            break;
        }
        if (!isMovingLeft && !isMovingRight && !isMovingUp && !isMovingDown){
            movement = Vector2.zero;
            rb.velocity = Vector2.zero;
            state = playerState.Idle;
        }
    }
    public void StartBlocking(){
        state = playerState.Parry;
        canAttack = false;
        moveSpeed /= 2;
        an.SetBool("block", true);
    }
    private void HoldingBlock(){
        state = playerState.Block;
        onBlocking?.Invoke(this, EventArgs.Empty);
    }
    public void Unblocking(){
        moveSpeed = defaultMovespeed;
        AnimationEnd();
    }
    public void EvadeMove(){
        if (attribute.DashStamina()){
            state = playerState.Evade;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            an.SetBool("evade", true);
            onDashing?.Invoke(this, EventArgs.Empty);
        }
    }
    public void NormalAttack(){
        if (attribute.NatkStamina() && state != playerState.StrongAttack && canAttack){
            if (normalAtkCombo >= maxComboAtk)
            normalAtkCombo = 0;
            normalAtkCombo ++;
            an.SetInteger("ncombo" , normalAtkCombo);
            canAttack = false;
            state = playerState.Attack;
            onNormalAttack?.Invoke(this, EventArgs.Empty);
        }
    }
    public void SkillState(){
        
    }
    private void FinishAttack(){
        canAttack = true;
        onAttackFinished?.Invoke(this, EventArgs.Empty);
    }
    public float GetChargeTime(){
        return chargeTime;
    }
    public void StartChargeAttack(){
        if (attribute.SatkStamina() && isPlayerFine()){
            isCharging = true;
            chargeTime = 0;
            InvokeRepeating(nameof(HoldingChargeAttack),0,0.1f);
            an.SetBool("isholding", true);
        }
    }
    private void HoldingChargeAttack(){
        if (isCharging){
            if (chargeTime < maxChargeTime)
            chargeTime += 0.1f;
        }
        an.SetBool("isholding", isCharging);
    }
    public void ReleaseChargeAttack(){
        if (isCharging && chargeTime > 0.5f){
            if (attribute.SatkStamina()){
                state = playerState.StrongAttack;
                an.SetBool("satk", true);
                canAttack = false;
                onChargeAttack?.Invoke(this, EventArgs.Empty);
            }else AnimationEnd();
        }
        isCharging = false;
        CancelInvoke(nameof(HoldingChargeAttack));
        an.SetBool("isholding", false);
    }
    private void AnimationEnd(){
        normalAtkCombo = 0;
        moveSpeed = defaultMovespeed;
        canAttack = true;
        if (state == playerState.Evade || state == playerState.Daze)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        state = playerState.Idle;
        rb.velocity = Vector2.zero;
        if (!isCharging){
            an.SetBool("isholding", false);
            an.SetBool("satk" , false);
        }
        an.SetInteger("ncombo", 0);
        an.SetBool("evade", false);
        an.SetBool("block", false);
        an.SetBool("daze", false);
    }
    private void CheckFallDamage(){
        if (isGround)
        {
            ApplyFallDamage();
        }
    }
    private void ApplyFallDamage()
    {
        if (rb.velocity.y < -10){
            float damageAmount = attribute.GetNumberByPercent(attribute.finalHP, Mathf.Abs(rb.velocity.y));
            attribute.TakeDamage(damageAmount, 0, "", transform);
        }
    }

    private void Jump(){
        if (isGround){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            attribute.lv.GainDEXxp(10);
            state = playerState.Jumping;
        }
    }
   private void OnCollisionStay2D(Collision2D coll){
        if (coll.gameObject.tag == "Ground"){
            isGround = true;
            if (state == playerState.Jumping && isJumping){
                isJumping = false;
                state = playerState.Idle;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D col){
        if (col.gameObject.tag == "Ground"){
            isGround = false;
            if (state == playerState.Jumping){
                isJumping = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Ladder"){
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Ladder"){
            isLadder = false;
        }
    }
    private void OnJumpButton(object sender, EventArgs e)
    {
        Jump();
    }
    public bool CanHitPlayer(){
        return !immortal;
    }
    public bool isPlayerFine(){
        return state != playerState.Attack && state != playerState.StrongAttack && state != playerState.Daze && state != playerState.Dead;
    }
    public void SkillButton(string skillSlot){
        skillDoing = skillSlot;
        onUsingSkill?.Invoke(this, EventArgs.Empty);
    }
    public void FinishSill(){
        state = playerState.Idle;
        onFinishedSkill?.Invoke(this, EventArgs.Empty);
    }
}
