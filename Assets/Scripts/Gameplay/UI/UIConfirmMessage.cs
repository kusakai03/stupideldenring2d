using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIConfirmMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private Action confirmAction;
    public void ShowConfirmMessage(string text, Action confirmAction){
        this.text.text = text;
        this.confirmAction = confirmAction;
        gameObject.SetActive(true);
    }
    public void OnConfirm()
    {
        confirmAction?.Invoke();
        gameObject.SetActive(false); 
    }
    public void OnCancel()
    {
        gameObject.SetActive(false);
    }
}
