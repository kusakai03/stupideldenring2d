using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemObtained : MonoBehaviour
{
    [SerializeField] private Image itemimg;
    [SerializeField] private TextMeshProUGUI itemname;
    public void Setvalue(Sprite img, string itemname){
        itemimg.sprite = img;
        this.itemname.text = itemname;
    }
}
