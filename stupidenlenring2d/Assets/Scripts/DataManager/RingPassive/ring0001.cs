using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ring0001 : RingPassive
{
    //Tăng 10% sát thương trong 10 giây sau khi tiêu diệt kẻ địch, cộng dồn 3 lần.
    private int stack;
    private int maxStack = 3;
    private int buffATK;
    private void Start(){
        EntityAttribute.onOutOfHealthStatic += OnOutOfHealthStatic;
        Passive();
    }
    private void OnDisable(){
        EntityAttribute.onOutOfHealthStatic -= OnOutOfHealthStatic;
    }

    private void OnOutOfHealthStatic(object sender, EventArgs e)
    {
        if (stack < maxStack){
            stack++;
            InvokeRepeating(nameof(Unstack), 0, 10);
        }
        Passive();
    }
    private void Unstack(){
        if (stack > 0){
            stack --;
            Passive();
        }
        else CancelInvoke(nameof(Unstack));
    }
    public override void Passive()
    {
        PlayerAttribute player = this.player.GetComponent<PlayerAttribute>();
        player.buffATK(-buffATK);
        buffATK = (int)player.GetNumberByPercent(player.finalATK, 10 * stack);
        player.buffATK(buffATK);
    }
}
