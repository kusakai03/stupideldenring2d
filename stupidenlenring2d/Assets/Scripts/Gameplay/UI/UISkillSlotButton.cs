using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillSlotButton : MonoBehaviour
{
    [SerializeField] private int index;
    private SkillData skill;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject lockSkillPanel;
    public void SetSkill(SkillData skill){
        if (SkillManager.Instance.GetSkillByID(skill.skillID)){
            this.skill = skill;
            icon.sprite = SkillManager.Instance.GetSkillByID(skill.skillID).skillIcon;
        }
    }
    public void OnClick(){
        PlayerManager.Instance.currentPlayer.GetComponent<PlayerMoving>().SkillButton("skill" + (index+1));
    }
    private void Update(){
        if (PlayerManager.Instance.currentPlayer)
        lockSkillPanel.SetActive(PlayerManager.Instance.currentPlayer?.GetComponent<PlayerAttribute>().currentMP <= skill?.skillMP);
    }
}
