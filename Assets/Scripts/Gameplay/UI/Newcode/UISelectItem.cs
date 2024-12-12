using System;
using UnityEngine;
using UnityEngine.UI;

public class UISelectItem : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private Image icon;
    public static Action<int> OnItemSelected;
    public void SetIcon(Sprite icon){
        this.icon.sprite = icon;
    }
    public void SetIndex(int index){
        this.index = index;
    }
    public void OnClick(){
        OnItemSelected?.Invoke(index);
    }
}
