using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillPageSlot : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private UISkillEquip page;
    private SkillData skill;
    [SerializeField] private Sprite blankSprite;
    [SerializeField] private Image skillIcon;
    private void OnEnable(){
        if (PlayerManager.Instance){
            skill = SkillManager.Instance.skillSlot[index];
            UpdateSlot();
        }
    }
    private void UpdateSlot(){
        if (skill != null){
            skillIcon.sprite = blankSprite;
        }
    }
    public void OnClick(){
        page.SelectSkillIndex(index);
    }
}
