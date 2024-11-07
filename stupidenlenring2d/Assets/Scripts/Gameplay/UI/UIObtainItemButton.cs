using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObtainItemButton : MonoBehaviour
{
    private ItemsObtain chest;
    public void SetValue(ItemsObtain itemDrop){
        this.chest = itemDrop;
    }
    public void OnClick(){
        chest.GainItems();
    }
    private void Update(){
        if (chest){
            if (!chest.player){
                gameObject.SetActive(false);
            }
        }
    }
}
