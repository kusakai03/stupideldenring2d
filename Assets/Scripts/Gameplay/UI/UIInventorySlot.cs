using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    private ItemOwned item;
    private UIInventory inventory;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantity;
    public void SetValue(ItemOwned item, UIInventory inv){
        this.item = item;
        inventory = inv;
        icon.sprite = ItemManager.ins.GetItemByid(item.itemID).itemSprite;
        quantity.text = item.quantity.ToString();
    }
    public void OnSelect(){
        inventory.SelectItem(item);
    }
}
