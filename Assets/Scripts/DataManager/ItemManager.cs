using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public static ItemManager ins{ get; private set ; }
    public static event EventHandler onItemUsed;
    public List<ItemOwned> itemOwneds;
    [SerializeField] private List<Itemdata> itemdatas;
    private void Awake(){
        if (ins == null){
            ins = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    private void Update(){
        if (SceneManager.GetActiveScene().name == "MainMenu") Destroy(gameObject);
    }
    public Itemdata GetItemByid(string id){
        return itemdatas.Find(x => x.itemID == id);
    }
    private bool IsItemExist(string id){
        return itemOwneds.Any(i => i.itemID == id);
    }
    public void RecieveItem(Itemdata item, int quantity){
        if (IsItemExist(item.itemID)){
            itemOwneds.FirstOrDefault(i => i.itemID == item.itemID).quantity += quantity;
        }
        else{
            itemOwneds.Add(new ItemOwned(item.itemID, quantity));
        }
    }
    public void RewardItems(List<ItemDrop> items){
        foreach(var i in items){
            if (i.isEquipment){
                PlayerManager.Instance.GetEquipment(i.itemID, i.quantityorlevel);
                UIItemObtainedList.Instance?.SpawnSomething(PlayerManager.Instance.GetEquipmentByID(i.itemID).eImage, "+ "+i.quantityorlevel + " " + PlayerManager.Instance.GetEquipmentByID(i.itemID).ename);
            }
            else{              
                RecieveItem(GetItemByid(i.itemID), i.quantityorlevel);
                UIItemObtainedList.Instance?.SpawnSomething(GetItemByid(i.itemID).itemSprite, "x"+i.quantityorlevel + " "+ GetItemByid(i.itemID).itemName);
            }
        }
    }
    public void LoseQuantity(string id, int quantity){
        if (GetItemByid(id)){
            if (IsItemExist(id)){
                ItemOwned i = itemOwneds.Find(x => x.itemID == id);
                if (i.quantity > quantity){
                    i.quantity -= quantity;
                }
            }
        }
    }
    public int GetQuantityByID(string id){
        if (IsItemExist(id)){
            ItemOwned i = itemOwneds.Find(x => x.itemID == id);
            return i.quantity;
        }
        return default;
    }
    public Sprite GetItemIcon(ItemDrop item){
        if (item.isEquipment){
            return PlayerManager.Instance.GetEquipmentByID(item.itemID).eImage;
        }
        else{
            return GetItemByid(item.itemID).itemSprite;
        }
    }
    public void UseItem(ItemOwned item) {
        Instantiate(GetItemByid(item.itemID).consumeItem);
        item.quantity--;
        if (item.quantity <= 0) itemOwneds.Remove(item);
        onItemUsed?.Invoke(this, EventArgs.Empty);
    }
    
}
[Serializable]
public class ItemOwned{
    public string itemID;
    public int quantity;

    public ItemOwned(string itemID, int quantity)
    {
        this.itemID = itemID;
        this.quantity = quantity;
    }
}
[Serializable]
public class ItemDrop{
    public string itemID;
    public bool isEquipment;
    public int quantityorlevel;
}
[Serializable]
public class ItemSell{
    public string itemID;
    public int cost;
}