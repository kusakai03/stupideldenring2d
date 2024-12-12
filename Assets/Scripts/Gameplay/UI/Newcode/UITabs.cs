using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabs : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    private int indexSelected;
    private void OnEnable(){
        UpdatePage();
    }
    private void OnDisable(){
    }

    private void UpdatePage(){
        foreach(var i in pages){
            i.SetActive(false);
        }
        pages[indexSelected].SetActive(true);
    }

    public void OnItemSelected(int obj)
    {
        indexSelected = obj;
        UpdatePage();
    }
}
