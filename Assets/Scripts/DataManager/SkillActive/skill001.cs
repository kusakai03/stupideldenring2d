using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill001 : SkillActive
{
    //Giải phóng sức mạnh cuối cùng, máu giảm xuống còn 1, tấn công cực mạnh về phía trước gây sát thương vật lí, 
    // Sau khi thi triển kĩ năng sẽ bị choáng và chịu trạng thái mù lòa.
    private int damage;
    public override void Start(){
        base.Start();
        //playerAttribute.currentWeapon.onWeaponHit += OnWeaponHit;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        //playerAttribute.currentWeapon.onWeaponHit -= OnWeaponHit;
    }

    private void OnWeaponHit(object sender, EventArgs e)
    {
        //if (doingSkill)
        //playerAttribute.currentWeapon.SetDamage(damage, 10);
    }

    public override void Skill()
    {
        PlayerSkill bskill = SkillManager.Instance.GetSkillByID(skill.skillID);
        playerAttribute.Healing((int)playerAttribute.currentHP-1);
        damage = (int)(playerAttribute.finalATK * bskill.skillValue1[skill.skillLevel] * 2.98f);
        //playerAttribute.currentWeapon.SetDamage(damage, 10);
    }
    public override void Active()
    {
        playerAttribute.ApplyStatusEffect("Blind", 9999999);
    }
}
