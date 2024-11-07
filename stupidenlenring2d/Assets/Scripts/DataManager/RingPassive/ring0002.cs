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
    }
    private void OnDisable(){
        PlayerAttribute p = player.GetComponent<PlayerAttribute>();
        p.effect.onEffectAdded -= OnEffectAdded;
        p.effect.onEffectRemoved -= OnEffectRemoved;
    }

    private void OnEffectRemoved(object sender, EventArgs e)
    {
        Passive();
    }

    private void OnEffectAdded(object sender, EventArgs e)
    {
        Passive();
    }
    public override void Passive()
    {
        PlayerAttribute p = player.GetComponent<PlayerAttribute>();
        if (p.effect.effects.Any(a => a.effectType == "Bleed")){
            p.buffATK(-patkBuff);
            patkBuff = (int)p.GetNumberByPercent(p.finalATK, 50);
            p.buffATK(patkBuff);
        }
    }
}
