using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ring0003 : RingPassive
{
    //Khi đỡ đòn, năng lượng Lá Chắn Viễn Cổ sẽ tiếp sức cho nhân vật, triệt tiêu mọi loại sát thuong, tiêu hao 200 MP.
    PlayerAttribute p;
    private void Start(){
        p = player.GetComponent<PlayerAttribute>();
        p.onDamageTakenCalculated += OnDamageTaken;
    }
    private void OnDisable(){
        p.onDamageTakenCalculated -= OnDamageTaken;
    }

    private void OnDamageTaken(object sender, EventArgs e)
    {
        Passive();
    }
    public override void Passive()
    {
        if (p.currentMP >= 200 && p.GetComponent<PlayerMoving>().state == PlayerMoving.playerState.Block){
            p.lastDamageTaken = 0;
            p.SpendMP(200);
        }
    }
}
