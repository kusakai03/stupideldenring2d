using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemObtainedList : MonoBehaviour
{
    public static UIItemObtainedList Instance { get; private set; }
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject spawn;
    private void Awake(){
        Instance = this;
    }
    public void SpawnSomething(Sprite sprite, string itemname){
        Instantiate(spawn, content).GetComponent<UIItemObtained>().Setvalue(sprite, itemname);
    }
}
