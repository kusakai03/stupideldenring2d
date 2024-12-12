using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDescription : MonoBehaviour
{
    private object item;
    public void SetValue(object item){
        this.item = item;
    }
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI iName;
    [SerializeField] private TextMeshProUGUI iType;
    [SerializeField] private TextMeshProUGUI iDescription;
}
