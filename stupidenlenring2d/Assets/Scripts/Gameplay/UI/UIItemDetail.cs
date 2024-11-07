using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDetail : MonoBehaviour
{
    private ItemOwned item;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject useItemButton;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemDescription;
    private bool useable;
    public void SetValue(ItemOwned item){
        this.item = item;
        Itemdata it = ItemManager.ins.GetItemByid(item.itemID);
        icon.sprite = it.itemSprite;
        itemName.text = it.itemName;
        itemType.text = it.itemType;
        itemDescription.text = it.itemDescription;
        useable = it.itemType == "consume";
        useItemButton.SetActive(useable);
    }

    public void UseItem(){
        ItemManager.ins.UseItem(item);
        if (item.quantity <= 0)
        gameObject.SetActive(false);
    }
}
