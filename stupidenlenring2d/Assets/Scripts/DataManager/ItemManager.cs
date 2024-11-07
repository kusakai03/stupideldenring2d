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
