using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set;}    
    public event EventHandler OnSkillChanged;
    [SerializeField] private List<PlayerSkill> skillList;
    [SerializeField] private List<SkillData> ownedSkills;
    public List<SkillData> skillSlot = new(new SkillData[3]);
    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);
        PlayerManager.Instance.onSpawnSuccess += OnPlayerSpawned;
    }
    private void OnDisable(){
        PlayerManager.Instance.onSpawnSuccess -= OnPlayerSpawned;
    }

    private void OnPlayerSpawned(object sender, EventArgs e)
    {
        PlayerSkillHolder skillHolder = PlayerManager.Instance.currentPlayer.GetComponentInChildren<PlayerSkillHolder>();
        for (int i = 1; i <= 3; i++){
            if (skillSlot[i-1] != null){
                skillHolder.ReplaceSkill(i, skillSlot[i-1]);
            }
        }
    }

    public void AddSkill(PlayerSkill skill){
        if (!ownedSkills.Any(x => x.skillID == skill.skillID)){
            ownedSkills.Add(new SkillData(skill.skillID, 1, skill.manaCost));
        }
        else{
            ownedSkills.First(x => x.skillID == skill.skillID).skillLevel ++;
        }
    }
    public PlayerSkill GetSkillByID(string skillID){
        return skillList.Find(x => x.skillID.Equals(skillID));
    }
    public List<SkillData> GetOwnedSkillList(){
        return ownedSkills;
    }
    public void SwitchSkill(int index, SkillData skill){
        if (index >= 0 && index < 3){
            skillSlot[index] = skill;
        }
    }
    public void UnequipSkill(int index){
        skillSlot[index] = null;
    }
    public bool isThisSkillEquipped(SkillData s){
        return skillSlot.Contains(s);
    }
}
[Serializable]
public class SkillData
{
    public string skillID;
    public int skillLevel;
    public int defaultskillMP;
    public int skillMP;
    public SkillData (string skillID, int skillLevel, int skillMP){
        this.skillID = skillID;
        this.skillLevel = skillLevel;
        defaultskillMP = skillMP;
        this.skillMP = defaultskillMP;
    }
}
