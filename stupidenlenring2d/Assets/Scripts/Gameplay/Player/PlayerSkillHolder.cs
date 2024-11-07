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
        if (SkillManager.Instance.GetSkillByID(skillData.skillID)){
            RemoveSkill(index);
            skills[index] = skillData;
            GameObject so = Instantiate(SkillManager.Instance.GetSkillByID(skillData.skillID).skillObject, transform);
            so.GetComponent<SkillActive>().SetValue(skillData, "skill" + (index+1), this);
            skillobjects[index] = so;
            OnSkillChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public void RemoveSkill(int index){
        if (skills[index] != null){
            Destroy(skillobjects[index]);
        }
        skills[index] = null;
        skillobjects[index] = null;
    }
}
