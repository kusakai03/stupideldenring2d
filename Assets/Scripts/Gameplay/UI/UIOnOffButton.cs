using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnOffButton : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public void SwitchButton(){
        target.SetActive(!target.activeSelf);
    }
}
