using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICloseButton : MonoBehaviour
{
    [SerializeField] private GameObject uiObject;
    public void OnClick(){
        uiObject.SetActive(false);
    }
}
