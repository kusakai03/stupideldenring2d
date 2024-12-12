using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skilluwp0001 : SkillActive
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject pyroObject;
    private bool buffActive;
    private int numberOfPyroObject;
    private Vector2 hitPosition;
    private float buffAmount;
    public List<GroundProjectileSpawn> spawners;

    //Kích hoạt kĩ năng: tăng hệ số sát thương của đòn đánh thường và trọng kích trong lần tấn công đầu tiên. 
    //Khi thực hiện đòn trọng kích của lần này, sẽ tạo ra năm cột lửa, gây sát thương hỏa, kèm theo thiêu đốt (30).
    public override void Start(){
        base.Start();
        buffActive = false;
        playerMoving.onAttackFinished += OnAttackFinished;
        playerAttribute.onWeaponUpdated += OnWeaponUpdated;
    }
    public override void OnDisable(){
        playerMoving.onAttackFinished -= OnAttackFinished;
        playerAttribute.onWeaponUpdated -= OnWeaponUpdated;
    }

    private void OnWeaponUpdated(object sender, EventArgs e)
    {
        Inactive();
        Active();
    }

    private void OnAttackFinished(object sender, EventArgs e)
    {
        if (buffActive){
            if (playerMoving.state == PlayerMoving.playerState.StrongAttack){
                numberOfPyroObject = 5;
                hitPosition = playerAttribute.currentWeapon.transform.position;
                SpawnPyroObject();
            }
        }
        Inactive();
    }
    private void SpawnPyroObject(){
        float side;
        side = player.transform.localScale.x < 0 ? -1 : 1;
        int damage = (int)(playerAttribute.finalMAG * SkillManager.Instance.GetSkillByID(skill.skillID).skillValue2[skill.skillLevel]);
        if (spawners.Count <= 0)
            for (int i = 0; i < numberOfPyroObject; i++){
                GroundProjectileSpawn pyroSpawn = Instantiate(spawner, new Vector2(hitPosition.x + side * (i * 1.5f), hitPosition.y), Quaternion.identity).GetComponent<GroundProjectileSpawn>();
                pyroSpawn.SetObject(pyroObject, damage);
                spawners.Add(pyroSpawn);
            }
        else{
            for (int j = 0; j < numberOfPyroObject; j++){
                spawners[j].gameObject.SetActive(true);
                spawners[j].SetObject(pyroObject, damage);
                spawners[j].transform.position = new Vector2(hitPosition.x + side * (j * 0.5f), hitPosition.y);
            }
        }
    }
    public override void Active()
    {
        if (!buffActive){
            buffAmount = SkillManager.Instance.GetSkillByID(skill.skillID).skillValue1[skill.skillLevel];
            buffActive = true;
            playerAttribute.dmgMultiplier += buffAmount;
        }
    }
    public override void Inactive()
    {
        if (buffActive){
            buffActive = false;
            playerAttribute.dmgMultiplier -= buffAmount;
            buffAmount = 0;
        }
    }
}
