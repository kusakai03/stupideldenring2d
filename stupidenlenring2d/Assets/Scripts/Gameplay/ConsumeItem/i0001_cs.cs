using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class i0001_cs : ConsumeItem
{
    [SerializeField] private int poison;
    public override void OnUsed()
    {
        player.GetComponent<PlayerBuffEffect>().AddEffect("Regeneration", 1, 10);
        player.GetComponent<PlayerAttribute>().ApplyStatusEffect("Poison", poison);
        gameObject.SetActive(false);
    }
}
