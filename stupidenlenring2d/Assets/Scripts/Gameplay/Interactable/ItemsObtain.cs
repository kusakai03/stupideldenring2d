using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsObtain : MonoBehaviour
{
    [SerializeField] private string chestid; //nếu không ghi gì thì đó là vật phẩm rơi ra từ quái
    [SerializeField] private bool isChest;
    public List<ItemDrop> items;
    public GameObject player;
    public void Start(){
        if (chestid != "" && PlayerManager.Instance.gainedChestIDs.Contains(chestid))
        gameObject.SetActive(false);    
    }
    public void GainItems(){
        foreach(var i in items){
            if (i.isEquipment){
                PlayerManager.Instance.GetEquipment(i.itemID, i.quantityorlevel);
                UIItemObtainedList.Instance.SpawnSomething(PlayerManager.Instance.GetEquipmentByID(i.itemID).eImage, "+ "+i.quantityorlevel + " " + PlayerManager.Instance.GetEquipmentByID(i.itemID).ename);
            }
            else{              
                ItemManager.ins.RecieveItem(ItemManager.ins.GetItemByid(i.itemID), i.quantityorlevel);
                UIItemObtainedList.Instance.SpawnSomething(ItemManager.ins.GetItemByid(i.itemID).itemSprite, "x"+i.quantityorlevel + " "+ ItemManager.ins.GetItemByid(i.itemID).itemName);
            }
        }
        if (chestid != "")
        PlayerManager.Instance.gainedChestIDs.Add(chestid);
        OpenChestAnimation();
    }
    public void OpenChestAnimation(){
        if (isChest){
            GetComponent<LoopSpriteAnimation>().Play();
        }
        else gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            player = collision.gameObject;
            MobileButton.Instance.ShowObtainButton(this);
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            player = null;
        }
    }
}
