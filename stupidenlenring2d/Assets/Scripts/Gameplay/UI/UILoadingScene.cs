using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoadingScene : MonoBehaviour
{
    private async void Start(){
        if (SceneLoader.Instance.sceneName == ""){
            await PlayerManager.Instance.LoadData();
            PlayerManager.Instance.Respawn();
        }
        else SceneManager.LoadSceneAsync(SceneLoader.Instance.sceneName);
    }
}
