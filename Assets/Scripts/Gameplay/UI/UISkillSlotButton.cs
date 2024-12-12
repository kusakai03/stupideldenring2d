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
    [SerializeField] private Sprite blankIcon;
    public void SetSkill(SkillData skill){
        PlayerSkill ps =  SkillManager.Instance.GetSkillByID(skill?.skillID);
        this.skill = skill;
        icon.sprite = ps? ps.skillIcon : blankIcon;
    }
    public void OnClick(){
        if (skill!=null)
        PlayerManager.Instance.currentPlayer.GetComponent<PlayerMoving>().SkillButton("skill" + (index+1));
    }
    private void Update(){
        if (PlayerManager.Instance.currentPlayer)
        lockSkillPanel.SetActive(PlayerManager.Instance.currentPlayer?.GetComponent<PlayerAttribute>().currentMP <= skill?.skillMP);
    }
}
