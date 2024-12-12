using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICarousel : MonoBehaviour
{
    [SerializeField] private GameObject[] page;
    private int currentPage;
    private void OnEnable(){
        currentPage = 0;
        ShowPage();
    }
    public void PrevPage(){
        if (currentPage <= 0){
            currentPage = page.Length -1;
        }
        else currentPage --;
        ShowPage();
    }
    public void NextPage(){
        if (currentPage >= page.Length -1)
        currentPage = 0;
        else currentPage ++;
        ShowPage();
    }
    private void ShowPage(){
        ClearPage();
        page[currentPage].SetActive(true);
    }
    private void ClearPage(){
        foreach (var page in page){
            page.SetActive(false);
        }
    }
}
