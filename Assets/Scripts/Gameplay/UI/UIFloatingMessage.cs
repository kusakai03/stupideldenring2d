using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFloatingMessage : MonoBehaviour
{
    public static UIFloatingMessage Instance { get; private set; }
    [SerializeField] private GameObject msg;
    private void Awake(){
        Instance = this;
    }
    public void ShowMessage(string message){
        GameObject m = Instantiate(msg, transform);
        m.GetComponent<TextMeshProUGUI>().text = message;
        m.SetActive(true);
    }
}
