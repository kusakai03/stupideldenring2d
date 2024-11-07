using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] idleState;
    [SerializeField] private Sprite[] runningState;
    [SerializeField] private Sprite[] jumpingState;
    [SerializeField] private Sprite[] dashingState;
    [SerializeField] private Sprite[] hurtState;
    [SerializeField] private Sprite[] deadState;
    [Header ("Weapon Attack Animation")]
    [SerializeField] private AnimationClip[] swordAnim; //"natk1", "natk2", "holdingcharge", "satk", "block"
    [SerializeField] private AnimationClip[] greatswordAnim;
    private PlayerAttribute at;
    private Animator animator;
    private PlayerMoving moving;
    private LoopSpriteAnimation spriteAnimation;
    private PlayerMoving.playerState currentState;
    private void Awake(){
        at = GetComponent<PlayerAttribute>();
        animator = GetComponent<Animator>();
        spriteAnimation = GetComponentInChildren<LoopSpriteAnimation>();
        moving = GetComponent<PlayerMoving>();
    }
    private void Start(){
        at.onWeaponUpdated += OnWeaponUpdated;
        currentState = moving.state;
        spriteAnimation.SetFrame(idleState, true, false, 0.2f);
        spriteAnimation.Play();
    }
    private void OnDisable(){
        at.onWeaponUpdated -= OnWeaponUpdated;
    }
    private void Update(){
        if (currentState != moving.state){
            switch (moving.state){
                case PlayerMoving.playerState.Idle:
                spriteAnimation.SetFrame(idleState, true, false, 0.5f);
                break;
                case PlayerMoving.playerState.Moving:
                spriteAnimation.SetFrame(runningState, true, false, 0.1f);
                break;
                case PlayerMoving.playerState.Jumping:
                spriteAnimation.SetFrame(jumpingState, false, false, 0.3f);
                break;
                case PlayerMoving.playerState.Evade:
                spriteAnimation.SetFrame(dashingState, true, false, 0.4f);
                break;
                case PlayerMoving.playerState.Daze:
                spriteAnimation.SetFrame(hurtState, false, false, 0.3f);
                break;
                case PlayerMoving.playerState.Dead:
                spriteAnimation.SetFrame(deadState, false, false, 0.4f);
                break;
            }
            spriteAnimation.Play();
            currentState = moving.state;
        }
    }

    private void OnWeaponUpdated(object sender, EventArgs e)
    {
        UpdateAnimation();
    }
    private void UpdateAnimation(){
        string[] statename = {"natk1", "natk2", "holdingcharge", "satk", "block"}; 
        if (at.weapon1 != null){
            switch (PlayerManager.Instance.GetEquipmentByID(at.weapon1.eid).weapon.weaponType){
                case "One Hand Sword":
                UpdateAnimationState(statename, swordAnim);
                break;
                case "Two Handed Sword":
                UpdateAnimationState(statename, greatswordAnim);
                break;
            }
        }
    }
    public void UpdateAnimationState(string[] animationState, AnimationClip[] clip){
        AnimatorOverrideController controller = new(animator.runtimeAnimatorController);
        for (int i = 0; i < animationState.Length; i++)
        controller[animationState[i]] = clip[i];
        animator.runtimeAnimatorController = controller;
    }
}
