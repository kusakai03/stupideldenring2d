using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIForgeEquipment : MonoBehaviour
{
    private List<EquipmentForge> eList;
    [SerializeField] private string moneyID;
    [SerializeField] private RectTransform listContent;
    [SerializeField] private GameObject selectItem;
    [SerializeField] private GameObject materialBox;
    [SerializeField] private GameObject materialItem;
    [SerializeField] private GameObject eNameBox;
    [SerializeField] private GameObject eAttributeBox;
    [SerializeField] private TextMeshProUGUI[] eAttBoxStats;
    [SerializeField] private GameObject eBlockStatBox;
    [SerializeField] private TextMeshProUGUI[] eBlockStatBoxStats;
    [SerializeField] private GameObject eResStatBox;
    [SerializeField] private TextMeshProUGUI[] eResStatBoxStat;
    [SerializeField] private GameObject eResStat1;
    [SerializeField] private GameObject eResStat2;
    [SerializeField] private GameObject ePassiveBox;
    [SerializeField] private TextMeshProUGUI ePassiveBoxText;
    [SerializeField] private RectTransform materialContent;
    [SerializeField] private TextMeshProUGUI forgeCost;
    [SerializeField] private TextMeshProUGUI currentCoin;
    private EquipmentForge e;
    private void OnEnable(){
        UISelectItem.OnItemSelected += OnItemSelected;
        UpdateEquipmentList();
        materialBox.SetActive(false);
        eNameBox.SetActive(false);
        eAttributeBox.SetActive(false);
        eBlockStatBox.SetActive(false);
        eResStatBox.SetActive(false);
        ePassiveBox.SetActive(false);
    }
    private void OnDisable(){
        UISelectItem.OnItemSelected -= OnItemSelected;
        e = null;
        foreach(Transform child in listContent){
            Destroy(child.gameObject);
        }
        foreach(Transform child in materialContent){
            if (child.gameObject != materialItem){
                Destroy(child.gameObject);
            }
        }
    }

    private void OnItemSelected(int obj)
    {
        e = eList[obj];
        CheckActive();
        ShowMaterials();
        ShowENameInfo();
        CheckAttributeBox();
        CheckBlockResBox();
        CheckPassiveBox();
    }

    public void SetEquipmentList(List<EquipmentForge> eList){
        this.eList = eList;
        UpdateEquipmentList();
    }
    private void UpdateEquipmentList(){
        if (eList != null){
            for (int i = 0; i < eList.Count; i++){
                UISelectItem s = Instantiate(selectItem, listContent).GetComponent<UISelectItem>();
                s.SetIndex(i);
                s.SetIcon(PlayerManager.Instance.GetEquipmentByID(eList[i].eid).eImage);
            }
        }
        currentCoin.text = ItemManager.ins.GetQuantityByID(moneyID).ToString();
    }
    private void CheckActive(){
        materialBox.SetActive(e != null);
        eNameBox.SetActive(e != null);
    }
    private void ShowMaterials(){
        foreach(Transform child in materialContent){
            if (child.gameObject != materialItem){
                Destroy(child.gameObject);
            }
        }
        for (int i = 0; i< e.materials.Count; i++){
            UISelectItem s = Instantiate(materialItem, materialContent).GetComponent<UISelectItem>();
            s.gameObject.SetActive(true);
            Sprite sp = ItemManager.ins.GetItemByid(e.materials[i].itemID).itemSprite;
            s.SetIcon(sp);
            int h = ItemManager.ins.GetQuantityByID(e.materials[i].itemID);
            s.GetComponentInChildren<TextMeshProUGUI>().text = h + "/" + e.materials[i].quantityorlevel;
        }
        forgeCost.text = "Rèn với "+e.cost.ToString();
    }
    public void Forge(){
        GameSetting.Instance.ShowConfirmMessage("Bạn có chắc chắn không?", ConfirmForge);
    }
    private void ConfirmForge(){
        bool isValid = true;
        foreach (var item in e.materials){
            if (!En(item.itemID, item.quantityorlevel)){
                isValid = false;
            }   
        }
        isValid = En(moneyID, e.cost);
        ItemManager i = ItemManager.ins;
        if (isValid){
            PlayerManager.Instance.GetEquipment(e.eid,1);
            i.LoseQuantity(moneyID, e.cost);
            foreach(var item in e.materials){
                i.LoseQuantity(item.itemID, item.quantityorlevel);
            }
            UpdateEquipmentList();
            ShowMaterials();
        }
        else{
            UIFloatingMessage.Instance.ShowMessage("Bạn chưa đủ vật phẩm");
        }
    }
    private void ShowENameInfo(){
        if (e != null){
            Equipment ed = PlayerManager.Instance.GetEquipmentByID(e.eid);
            string type = ed.etype == "Weapon"? ed.weapon.weaponType: ed.etype;
            eNameBox.GetComponent<UIItemDetail>().SetItem(ed.eImage, ed.ename, type, ed.edesc);
        }
    }
    private void CheckAttributeBox(){
        if (e != null){
            Equipment ep = PlayerManager.Instance.GetEquipmentByID(e.eid);
            eAttributeBox.SetActive(true);
            eAttBoxStats[0].text = ep.vitality.ToString();
            eAttBoxStats[1].text = ep.strength.ToString();
            eAttBoxStats[2].text = ep.dexerity.ToString();
            eAttBoxStats[3].text = ep.dexerity.ToString();
        }
    }
    private void CheckBlockResBox(){
        if (e != null){
            Equipment ep = PlayerManager.Instance.GetEquipmentByID(e.eid);
            eBlockStatBox.SetActive(ep.etype == "weapon");
            if (ep.etype == "Weapon"){
                eBlockStatBoxStats[0].text = ep.poise.ToString();
                eBlockStatBoxStats[1].text = ep.weapon.physicalRes.ToString() + "/100";
                eBlockStatBoxStats[2].text = ep.weapon.fireRes.ToString() + "/100";
                eBlockStatBoxStats[3].text = ep.weapon.iceRes.ToString() + "/100";
                eBlockStatBoxStats[4].text = ep.weapon.lightningRes.ToString() + "/100";
                eBlockStatBoxStats[5].text = ep.weapon.magicRes.ToString() + "/100";
            }
            List<string> armorType = new(){"armor", "glove", "helmet", "leg"};
            eResStatBox.SetActive(armorType.Contains(ep.etype));
            eResStatBoxStat[0].text = ep.poise.ToString();
            eResStatBoxStat[1].text = ep.physicalRES.ToString() + "/1000";
            eResStatBoxStat[2].text = ep.fireRES.ToString() + "/1000";
            eResStatBoxStat[3].text = ep.iceRES.ToString() + "/1000";
            eResStatBoxStat[4].text = ep.lightningRES.ToString() + "/1000";
            eResStatBoxStat[5].text = ep.magicRES.ToString() + "/1000";
            eResStatBoxStat[6].text = ep.poison.ToString() + "/1000";
            eResStatBoxStat[7].text = ep.burn.ToString() + "/1000";
            eResStatBoxStat[8].text = ep.frozen.ToString() + "/1000";
            eResStatBoxStat[9].text = ep.bleed.ToString() + "/1000";
            eResStatBoxStat[10].text = ep.blind.ToString() + "/1000";
        }
    }

    private void CheckPassiveBox(){
        if (e!= null){
            Equipment ep = PlayerManager.Instance.GetEquipmentByID(e.eid);
            ePassiveBox.SetActive(ep);
            if (ep.etype == "weapon"){
                ePassiveBoxText.text = ep.weapon.weaponSkill.skillDescription;
            }
            else if (ep.etype == "ring")
            {ePassiveBoxText.text = ep.passiveDesc;}
            else ePassiveBoxText.text = String.Empty;
        }
    }

    public void SwitchStatInResBox(){
        if (eResStatBox.activeInHierarchy == true){
            eResStat1.SetActive(!eResStat1.activeInHierarchy);
            eResStat2.SetActive(!eResStat2.activeInHierarchy);
        }
    }

    private bool En(string id, int quantity){
        return quantity <= ItemManager.ins.GetQuantityByID(id);
    }
}
