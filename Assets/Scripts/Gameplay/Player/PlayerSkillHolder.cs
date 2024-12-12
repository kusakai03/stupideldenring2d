using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillHolder : MonoBehaviour
{
    public List<SkillData> skills = new(new SkillData[4]);
    public List<GameObject> skillobjects = new(new GameObject[4]);
    public event EventHandler OnSkillChanged;
    public void ReplaceSkill(int index, SkillData skillData){
        if (skillData != null && SkillManager.Instance.GetSkillByID(skillData.skillID)){
            RemoveSkill(index);
            GameObject so = Instantiate(SkillManager.Instance.GetSkillByID(skillData.skillID).skillObject, transform);
            so.GetComponent<SkillActive>().SetValue(skillData, "skill" + (index+1), this);
            skillobjects[index] = so;
        }
        skills[index] = skillData;
        PlayerUpdateAnimationSkill();
        OnSkillChanged?.Invoke(this, EventArgs.Empty);
    }
    private void PlayerUpdateAnimationSkill(){
        string[] triggers = {"s1", "s2", "s3", "s4"};
        List<AnimationClip> skillAnim = new();
        List<string> triggerNotNull = new();
        for (int i = 0; i < skills.Count; i++) 
        {
            var animationClip = A(skills[i])?.skillAnimation;
            if (animationClip != null)
            {
                triggerNotNull.Add(triggers[i]);
                skillAnim.Add(animationClip);
            }
        }
        GetComponentInParent<PlayerAnimation>().UpdateAnimationState(triggerNotNull.ToArray(), skillAnim.ToArray());
    }
    private PlayerSkill A(SkillData s){
        return SkillManager.Instance.GetSkillByID(s.skillID);
    }
    public void RemoveSkill(int index){
        if (skills[index] != null){
            Destroy(skillobjects[index]);
        }
        skills[index] = null;
        skillobjects[index] = null;
    }
}
