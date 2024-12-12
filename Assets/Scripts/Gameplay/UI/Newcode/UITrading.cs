using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITrading : MonoBehaviour
{
    private List<ItemSell> tradingItems;
    private List<ItemOwned> itemOwneds;
    public List<GameObject> itemSlots;
    private ItemSell selectedItemSell;
    private ItemOwned selectedItemOwned;
    private int currentPage;
    public int numberOfItem;
    [SerializeField] private int itemPerPage;
    [SerializeField] private string moneyID;
    private bool buyingOrSelling; //false là mua, true là bán
    private int totalCost;
    [Header("For UI Element")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private TextMeshProUGUI itemTotalCost;
    [SerializeField] private TextMeshProUGUI numberTobuy;
    [SerializeField] private TextMeshProUGUI currentMoney;
    [SerializeField] private TextMeshProUGUI buymodeText;
    private void OnEnable(){
        UISelectItem.OnItemSelected += OnItemSelected;
        itemOwneds = ItemManager.ins.itemOwneds;
        currentMoney.text = ItemManager.ins.GetQuantityByID(moneyID).ToString();
        selectedItemOwned = itemOwneds[0];
        buymodeText.text = !buyingOrSelling? "Mua đồ" : "Bán đồ";
    }
    private void OnDisable(){
        UISelectItem.OnItemSelected -= OnItemSelected;
        selectedItemSell = null;
        selectedItemOwned = null;
    }

    private void OnItemSelected(int index)
    {
        if (!buyingOrSelling){
            Debug.Log(index);
            selectedItemSell = tradingItems[index];
        }
        else selectedItemOwned = itemOwneds[index];
        numberOfItem = 1;
        CalTotalCost();
        UpdateUIItem();
    }

    public void SetItemToSell(List<ItemSell> items){
        tradingItems = items;
        selectedItemSell = tradingItems[0];
    }
    public void UpdateItemPage(){
        currentPage = 1;
        foreach (var s in itemSlots){
            s.SetActive(false);
        }
        int startIndex = (currentPage - 1) * itemPerPage;
        int listCount = !buyingOrSelling ? tradingItems.Count : itemOwneds.Count;
        int endIndex = Mathf.Min(startIndex + itemPerPage, listCount);
        if (buyingOrSelling){
            for (int i = startIndex; i < endIndex; i++){
                itemSlots[i - startIndex].SetActive(true);
                itemSlots[i - startIndex].GetComponent<UISelectItem>().SetIcon(ItemManager.ins.GetItemByid(itemOwneds[i].itemID).itemSprite);
                itemSlots[i - startIndex].GetComponentInChildren<TextMeshProUGUI>().text = ItemManager.ins.GetItemByid(itemOwneds[i].itemID).sell.ToString();
            }
        }
        else{
            for (int i = startIndex; i < endIndex; i++){
                itemSlots[i - startIndex].SetActive(true);
                itemSlots[i - startIndex].GetComponent<UISelectItem>().SetIcon(ItemManager.ins.GetItemByid(tradingItems[i].itemID).itemSprite);
                itemSlots[i - startIndex].GetComponentInChildren<TextMeshProUGUI>().text = tradingItems[i].cost.ToString();
            }
        }
        UpdateUIItem();
    }
    private void UpdateUIItem(){
        string id = !buyingOrSelling? selectedItemSell.itemID : selectedItemOwned.itemID;
        Itemdata i = ItemManager.ins.GetItemByid(id);
        itemName.text = i.itemName;
        itemIcon.sprite = i.itemSprite;
        itemDesc.text = i.itemDescription;
        itemType.text = i.itemType;
    }
    public void OnPreviousPage(){
        if (currentPage > 1)
        {
            currentPage --;
            UpdateItemPage();
        }
    }
    public void OnNextPage(){
        int listCount = !buyingOrSelling ? tradingItems.Count : itemOwneds.Count;
        if ((currentPage - 1) * itemPerPage + itemPerPage < listCount)
        {
            currentPage++;
            UpdateItemPage();
        }
    }
    public void AddNumber(){
        numberOfItem++;
        if (buyingOrSelling){
            if (numberOfItem > selectedItemOwned.quantity)
            numberOfItem --;
        }
        CalTotalCost();
    }
    public void NegNumber(){
        if (numberOfItem > 0)
        numberOfItem--;
        CalTotalCost();
    }
    public void CalTotalCost(){
        if (!buyingOrSelling){
            totalCost = numberOfItem * selectedItemSell.cost;
        }
        else totalCost = numberOfItem * ItemManager.ins.GetItemByid(selectedItemOwned.itemID).sell;
        itemTotalCost.text = totalCost.ToString();
        numberTobuy.text = numberOfItem.ToString();
    }
    public void SwitchTradingMode(){
        buyingOrSelling = !buyingOrSelling;
        UpdateItemPage();
        UpdateUIItem();
        CalTotalCost();
        buymodeText.text = !buyingOrSelling? "Mua đồ" : "Bán đồ";
    }
    public void BuyOrSellButton(){
        GameSetting.Instance.ShowConfirmMessage("Bạn có muốn " +buymodeText.text+" không?", ConfirmBuyOrSell);
    }
    private void ConfirmBuyOrSell(){
        ItemManager im = ItemManager.ins;
        if (!buyingOrSelling){ //mua 
            if (totalCost > im.GetQuantityByID(moneyID)){
                UIFloatingMessage.Instance.ShowMessage("Bạn không đủ tiền");
            }
            else{
                im.RecieveItem(im.GetItemByid(selectedItemSell.itemID), numberOfItem);
                im.LoseQuantity(moneyID, totalCost);
            }
        }else{ //bán
            im.LoseQuantity(selectedItemOwned.itemID, numberOfItem);
            im.RecieveItem(im.GetItemByid(moneyID), totalCost);
        }
        currentMoney.text = ItemManager.ins.GetQuantityByID(moneyID).ToString();
    }
}
