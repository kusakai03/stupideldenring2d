using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill_smash : SkillActive
{
    private float damage;
    public override void Skill()
    {
        damage = playerAttribute.finalATK * SkillManager.Instance.GetSkillByID(skill.skillID).skillValue1[skill.skillLevel];
        playerAttribute.currentWeapon.GetComponent<WeaponPhysic>().SetDamage(damage, 120);
    }
}
