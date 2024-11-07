using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillPageObject : MonoBehaviour
{
    private SkillData skill;
    private UISkillEquip master;
    [SerializeField] private GameObject eState;
    [SerializeField] private Image skillicon;
    public void SetValue(SkillData skill, UISkillEquip master){
        this.skill = skill;
        this.master = master;
        skillicon.sprite = SkillManager.Instance.GetSkillByID(skill.skillID).skillIcon;
        eState.SetActive(SkillManager.Instance.skillSlot.Contains(skill));
    }
    public void OnClick(){
        if (master){
            master.SelectSkill(skill);
        }
    }
}
