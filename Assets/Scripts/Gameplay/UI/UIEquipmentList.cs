using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipmentList : MonoBehaviour
{
    //Tạo danh sách vật phẩm tùy theo loại, mỗi trang hiện 12 vật phẩm
    //Bấm nút để chuyển trang
    //Bấm vật phẩm để hiện thông tin
    [SerializeField] private int itemPerPage;
    private int currentPage;
    private string typeSelected;
    [SerializeField] private GameObject[] slots;
    public List<EquipmentData> listEByType;
    [SerializeField] private UIEquipmentDetail detail;
    private EquipmentData currentSelected;
    private void OnEnable(){
        currentPage = 1;   
    }
    private void SetList(){
        PlayerManager p = PlayerManager.Instance;
        listEByType.Clear();
        string equipType;
        equipType = typeSelected;
        if (typeSelected == "weapon1" || typeSelected == "weapon2")
            equipType = "weapon";
        if (typeSelected.Contains("ring"))
            equipType = "ring";
        foreach (var e in p.ownedEquipments){
            if (e.eType == equipType){
                listEByType.Add(e);
            }
        }
        UpdateItemPage();
        detail.gameObject.SetActive(false);
    }
    private void UpdateItemPage(){
        int startIndex = (currentPage - 1) * itemPerPage;
        int endIndex = Mathf.Min(startIndex + itemPerPage, listEByType.Count);
        foreach (var s in slots){
            s.SetActive(false);
        }
        for (int i = startIndex; i < endIndex; i++){
            slots[i - startIndex].SetActive(true);
            slots[i - startIndex].GetComponent<UIEquipmentSlotPage>().SetValue(listEByType[i], this);
        }
    }
    public void OnPreviousPage(){
        if (currentPage > 1)
        {
            currentPage --;
            UpdateItemPage();
        }
    }
    public void OnNextPage(){
        if ((currentPage - 1) * itemPerPage + itemPerPage < listEByType.Count)
        {
            currentPage++;
            UpdateItemPage();
        }
    }
    public void SetCurrentEquipping(EquipmentData equipment){
        currentSelected = equipment;
        SetList();
        if (PlayerManager.Instance.GetEquipmentByID(equipment?.eid)){
            detail.SetValue(currentSelected);
            detail.gameObject.SetActive(true);
        }
    }
    public void ShowEquipmentDetail(EquipmentData equipment){
        detail.SetValue(equipment);
        detail.gameObject.SetActive(true);
    }
    public void SetListType(string type){
        typeSelected = type;
    }
    public EquipmentData GetCurrentSelected(){
        return currentSelected;
    }
    public string GetSelectedType(){
        return typeSelected;
    }
}
