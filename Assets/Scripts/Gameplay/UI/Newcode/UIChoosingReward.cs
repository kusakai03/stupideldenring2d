using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChoosingReward : MonoBehaviour
{
    private ItemDrop itemSelected;
    [SerializeField] private List<GameObject> itemBoxes;
    [SerializeField] private List<ItemDrop> itemDrops;
    private void OnEnable(){
        UISelectItem.OnItemSelected += OnItemSelected;
    }
    private void OnDisable(){
        UISelectItem.OnItemSelected -= OnItemSelected;
        itemSelected = null;
        itemDrops = null;
    }
    private void OnItemSelected(int index)
    {
        itemSelected = itemDrops[index];
    }

    public void ShowRewardChoosingBox(List<ItemDrop> items){
        gameObject.SetActive(true);
        HideAllItemBox();
        itemDrops = items;
        for (int i = 0; i < items.Count; i++){
            itemBoxes[i].SetActive(true);
            itemBoxes[i].GetComponent<UISelectItem>().SetIcon(ItemManager.ins.GetItemIcon(items[i]));
        }
    }
    private void HideAllItemBox(){
        foreach(var x in itemBoxes){
            x.gameObject.SetActive(false);
        }
    }
    public void ConfirmItemButton(){
        GameSetting.Instance.ShowConfirmMessage("Bạn có chắc là chọn vật phẩm này?", ConfirmItem);
    }
    private void ConfirmItem(){
        List<ItemDrop> items = new List<ItemDrop>{itemSelected};
        ItemManager.ins.RewardItems(items);
        gameObject.SetActive(false);
        PlayerManager.Instance.SaveData();
    }
}
