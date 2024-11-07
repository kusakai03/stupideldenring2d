using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuffEffect : MonoBehaviour
{
    private PlayerAttribute att;
    private PlayerMoving mov;
    public event EventHandler onEffectAdded;
    public event EventHandler onEffectRemoved;
    private int healingDebuff;
    [Header ("Effect Icon")]
    [SerializeField] private Sprite regeneration;   
    [SerializeField] private Sprite poison;
    [SerializeField] private Sprite bleed;
    [SerializeField] private Sprite blind;
    [Header ("Effect List")]
    public List<StatusEffect> effects;
    private void Awake(){
        att = GetComponent<PlayerAttribute>();
        mov = GetComponent<PlayerMoving>();
    }
    public void AddEffect(string effectName, float effectAmount, int effectDuration){
        effects.Add(new StatusEffect(effectName,effectAmount, effectDuration, GetEffectImage(effectName)));
        GameObject icon = Instantiate(new GameObject(),UIGameplayBar.Instance.statusEffectBar);
        icon.AddComponent<Image>().sprite = GetEffectImage(effectName);
        icon.AddComponent<LifeTime>().SetLifeTime(effectDuration);
        InvokeRepeating(nameof(Effective),0,1);
        onEffectAdded?.Invoke(this, EventArgs.Empty);
    }
    private void Effective(){
        List<StatusEffect> removeEffects = new();
        foreach (var e in effects){
            switch(e.effectType){
                case "Regeneration":
                att.Healing((int)att.GetNumberByPercent(att.finalHP, 2));
                break;
                case "Poison":
                att.TakeDamage((int)att.GetNumberByPercent(att.finalHP, 1), 1, "",null);
                break;
                case "Bleed":
                att.healingBonus += healingDebuff;
                healingDebuff = att.healingBonus /2;
                att.healingBonus -= healingDebuff;
                break;
                case "Blind":
                FadeUI fade = GameObject.FindGameObjectWithTag("Blind").GetComponent<FadeUI>();
                if (!fade.isFading){
                    fade.SetValue(1, 10);
                    fade.Fade();
                }
                break;
            }
            e.effectDuration --;
            if (e.effectDuration <=0){
                removeEffects.Add(e);
            }
        }
        foreach (var e in removeEffects) {
            effects.Remove(e);
            onEffectRemoved?.Invoke(this, EventArgs.Empty);
        }
        if (effects.Count == 0){
            CancelInvoke(nameof(Effective));
        }
    }
    private Sprite GetEffectImage(string type){
        switch(type){
            case "Regeneration":
            return regeneration;
            case "Poison":
            return poison;
            case "Bleed":
            return bleed;
            case "Blind":
            return blind;
        }
        return default;
    }
}
[Serializable]
public class StatusEffect{
    public string effectType;
    public float effectAmount;
    public int effectDuration;
    public Sprite effectImage;
    public StatusEffect (string effectType, float effectAmount, int effectDuration, Sprite effectImage){
        this.effectType = effectType;
        this.effectAmount = effectAmount;
        this.effectDuration = effectDuration;
        this.effectImage = effectImage;
    }
}