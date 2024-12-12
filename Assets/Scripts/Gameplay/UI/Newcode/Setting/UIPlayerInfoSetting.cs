using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlayerInfoSetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI timeplayed;
    [SerializeField] private TextMeshProUGUI mapName;
    private void OnEnable(){
        if (PlayerManager.Instance){
            playerName.text = PlayerManager.Instance.playerName;
            //timeplayed.text = ?;
            mapName.text = SceneManager.GetActiveScene().name;
        }
        else{
            UIFloatingMessage.Instance.ShowMessage("Cần tham gia vào trò chơi để xem mục này");
        }
    }
    public void OnReturnButton(){
        GameSetting.Instance.ShowConfirmMessage("Bạn có chắc là muốn trở về menu chính không? \n "+
        "Những thay đổi chưa được lưu sẽ mất đi.", ConfirmReturn);
    }
    private void ConfirmReturn(){
        SceneLoader.Instance.LoadScene("Menu");
    }
    private void OnDisable(){
        playerName.text = "";
            //timeplayed.text = ?;
        mapName.text = "";
    }
}
