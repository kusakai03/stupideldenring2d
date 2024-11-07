using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSkill : ScriptableObject
{
    [Header("Basic Info")]
    public string skillID;
    public string skillName;
    public Sprite skillIcon;
    [TextArea]
    public string skillDescription;
    public AnimationClip skillAnimation;
    public GameObject skillObject;
    [Header("Skill Value")]
    public float[] skillValue1;
    public float[] skillValue2;
    public float[] skillValue3;
    public float[] skillValue4;
    public float[] skillValue5;
    public string skillElement;
    public GameObject skillEffect;
    public float duration;
    [Header ("Condition")]
    public int manaCost;
    public string requiredWeapon;
    public PlayerMoving.playerState requiredState;
}
