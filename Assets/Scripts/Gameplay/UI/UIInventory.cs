using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private string itemType = "consume";
    [SerializeField] private ItemOwned itemSelected;
    [SerializeField] private int itemPerPage;
    [SerializeField] private GameObject[] itemSlots;
    [SerializeField] private GameObject itemDetail;
    [SerializeField] private TextMeshProUGUI itemPageText;
    public List<ItemOwned> listItemByType;
    private int currentPage = 1;
    private void OnEnable(){
        ItemManager.onItemUsed += OnItemUsed;
        UpdateItems();
    }
    private void OnDisable(){
        itemDetail.SetActive(false);
        ItemManager.onItemUsed -= OnItemUsed;
    }
    private void OnItemUsed(object sender, EventArgs e)
    {
        UpdateItems();
    }

    private void UpdateItems(){
        ItemManager i = ItemManager.ins;
        listItemByType.Clear();
        if (i.itemOwneds.Count > 0)
        foreach (ItemOwned item in i.itemOwneds){
            if (i.GetItemByid(item.itemID).itemType == itemType){
                listItemByType.Add(item);
            }
        }
        UpdateUI();
    }
    private void UpdateUI(){
        int startIndex = (currentPage - 1) * itemPerPage;
        int endIndex = Mathf.Min(startIndex + itemPerPage, listItemByType.Count);
        foreach (var s in itemSlots){
            s.SetActive(false);
        }
        for (int i = startIndex; i < endIndex; i++){
            itemSlots[i - startIndex].SetActive(true);
            itemSlots[i - startIndex].GetComponent<UIInventorySlot>().SetValue(listItemByType[i], this);
        }
        itemPageText.text = currentPage + " / " + ((int)(listItemByType.Count / itemPerPage) + 1);
    }
    public void OnPreviousPage(){
        if (currentPage > 1)
        {
            currentPage --;
            UpdateUI();
        }
    }
    public void OnNextPage(){
        if ((currentPage - 1) * itemPerPage + itemPerPage < listItemByType.Count)
        {
            currentPage++;
            UpdateUI();
        }
    }
    public void SetItemType(string type){
        itemType = type;
        UpdateItems();
    }
    public void SelectItem(ItemOwned item){
        itemSelected = item;
        itemDetail.SetActive(true);
        itemDetail.GetComponent<UIItemDetail>().SetValue(item);
    }
}
