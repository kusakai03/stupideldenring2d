using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISkillPageDetail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sname;
    [SerializeField] private TextMeshProUGUI sdesc;
    [SerializeField] private TextMeshProUGUI sweapon;
    [SerializeField] private TextMeshProUGUI smana;
    private SkillData skill;
    public void SetValue(SkillData skill){
        gameObject.SetActive(true);
        this.skill = skill;
        UpdateUI();
    }
    private void UpdateUI(){
        PlayerSkill s = SkillManager.Instance.GetSkillByID(skill.skillID);
        sname.text = "(+"+skill.skillLevel.ToString() + ") "+s.skillName;
        sdesc.text = s.skillDescription;
        sweapon.text = "Yêu cầu " + s.requiredWeapon;
        smana.text = s.manaCost + " MP";
    }
}
