using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHidePlayerSetting : MonoBehaviour
{
    [SerializeField] private GameObject playerSetting;
    private void OnEnable(){
        playerSetting.SetActive(PlayerManager.Instance);
    }
}
