using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActive : MonoBehaviour
{
    protected SkillData skill;
    protected string skilSlot;
    protected PlayerSkillHolder holder;
    protected GameObject player;
    protected PlayerMoving playerMoving;
    protected PlayerAttribute playerAttribute;
    protected bool doingSkill;
    private void Awake(){
        player = PlayerManager.Instance.currentPlayer;
        playerMoving = player.GetComponent<PlayerMoving>();
        playerAttribute = player.GetComponent<PlayerAttribute>();
    }
    public virtual void Start(){
        playerMoving.onUsingSkill += OnDoingSkill;
        playerMoving.onFinishedSkill += OnFinishedSkill;
    }
    public virtual void OnDisable(){
        playerMoving.onUsingSkill -= OnDoingSkill;
        playerMoving.onFinishedSkill -= OnFinishedSkill;
    }
    public void SetValue(SkillData skill, string skilSlot, PlayerSkillHolder holder){
        this.skill = skill;
        this.holder = holder;
        this.skilSlot = skilSlot;
    }
    private void OnFinishedSkill(object sender, EventArgs e)
    {
        doingSkill = false;
        Active();
    }

    private void OnDoingSkill(object sender, EventArgs e)
    {
        if (playerAttribute.currentMP >= skill.skillMP){
            doingSkill = true;
            PlayAnimation(skilSlot);
            playerAttribute.SpendMP(skill.skillMP);
        }
    }

    public void PlayAnimation(string trigger){
        if (playerMoving.state == SkillManager.Instance.GetSkillByID(skill.skillID).requiredState && playerMoving.skillDoing == trigger && playerMoving.isPlayerFine()){
            player.GetComponent<Animator>().SetTrigger(trigger);
            playerMoving.state = PlayerMoving.playerState.Attack;
            Skill();
        }
    }
    public virtual void Skill(){ //Trạng thái skill trong lúc thi triển
        
    }
    //Active này gọi trong animation skill
    public virtual void Active(){ //Trạng thái skill sau khi thi triển

    }
    public virtual void Inactive(){

    }
}
