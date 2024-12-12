using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ring0002 : RingPassive
{
    private int patkBuff;
    //Khi nhân vật bị chảy máu, kích hoạt Cơn Thịnh Nộ Chiến Thần: tăng 50% tấn công vật lí.
    private void Start(){
        PlayerAttribute p = player.GetComponent<PlayerAttribute>();
        p.effect.onEffectAdded += OnEffectAdded;
        p.effect.onEffectRemoved += OnEffectRemoved;
        p.onAttributeUpdated += OnEffectAdded;
    }
    private void OnDisable(){
        PlayerAttribute p = player.GetComponent<PlayerAttribute>();
        p.effect.onEffectAdded -= OnEffectAdded;
        p.effect.onEffectRemoved -= OnEffectRemoved;
        p.onAttributeUpdated -= OnEffectAdded;
    }

    private void OnEffectRemoved(object sender, EventArgs e)
    {
        RemoveBuff();
    }

    private void OnEffectAdded(object sender, EventArgs e)
    {
        Passive();
    }
    public override void Passive()
    {
        PlayerAttribute p = player.GetComponent<PlayerAttribute>();
        if (patkBuff != 0)
            p.buffATK(-patkBuff);
            patkBuff = 0;
        if (p.effect.effects.Any(a => a.effectType == "Bleed")){
            patkBuff = (int)p.GetNumberByPercent(p.finalATK, 50);
            p.buffATK(patkBuff);
        }
    }
    private void RemoveBuff(){
        PlayerAttribute p = player.GetComponent<PlayerAttribute>();
        if (!p.effect.effects.Any(a => a.effectType == "Bleed")){
            if (patkBuff != 0)
            p.buffATK(-patkBuff);
            patkBuff = 0;
        }
    }
}
