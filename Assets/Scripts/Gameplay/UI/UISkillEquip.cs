using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillEquip : MonoBehaviour
{
    private int indexSelected;
    private SkillData skillSelected;
    [SerializeField] private GameObject skillDetail;
    [SerializeField] private GameObject[] skillListSlots;
    private int skillListFirstIndex;
    private List<SkillData> datas;
    private void OnDisable(){
        indexSelected = 0;
        skillSelected = null;
    }
    public void UpdateSkillList(){
        datas = SkillManager.Instance.GetOwnedSkillList();
        ShowSkills();
    }
    private void ShowSkills(){
        int startIndex = skillListFirstIndex;
        int endIndex = Mathf.Min(startIndex + skillListSlots.Length, datas.Count);
        foreach (var e in skillListSlots){
            e.SetActive(false);
        }
        for (int i = startIndex; i < endIndex; i++){
            skillListSlots[i].SetActive(true);
            skillListSlots[i].GetComponent<UISkillPageObject>().SetValue(datas[i],this);
        }
    }
    public void SwipeLeft(){
        if (datas.Count > skillListFirstIndex + skillListSlots.Length){
            skillListFirstIndex++;
            ShowSkills();
        }
    }
    public void SwipeRight(){
        if (datas.Count > 0 && skillListFirstIndex > 0){
            skillListFirstIndex--;
            ShowSkills();
        }
    }
    public void SelectSkillIndex(int skillIndex){
        indexSelected = skillIndex;
    }
    public void SelectSkill(SkillData skill){
        skillSelected = skill;
        skillDetail.GetComponent<UISkillPageDetail>().SetValue(skill);
    }
    public void SwitchSkill(){
        if (skillSelected != null && indexSelected != -1 && indexSelected < 3){
            SkillManager.Instance.SwitchSkill(indexSelected, skillSelected);
        }
    }
    public void ConfirmEquip(){
        bool isE = SkillManager.Instance.isThisSkillEquipped(skillSelected);
        PlayerSkill skill = SkillManager.Instance.GetSkillByID(skillSelected.skillID);
        if (isE){
            SkillManager.Instance.UnequipSkill(SkillManager.Instance.skillSlot.IndexOf(skillSelected));
            UIFloatingMessage.Instance.ShowMessage("Sẽ không dùng kĩ năng "+ skill.skillName);
        }
        else {
            SwitchSkill();
            UIFloatingMessage.Instance.ShowMessage("Sử dụng kĩ năng "+ skill.skillName);
        }
    }
}
