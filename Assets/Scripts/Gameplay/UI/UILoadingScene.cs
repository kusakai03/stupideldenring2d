using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoadingScene : MonoBehaviour
{
    private async void Start(){
        SceneLoader s = SceneLoader.Instance;
        if (s.sceneName == ""){
            await PlayerManager.Instance.LoadData();
            PlayerManager.Instance.Respawn();
        }
        else{
            if (!s.hasPlayer)
            SceneManager.LoadSceneAsync(s.sceneName);
            else{
                PlayerManager.Instance.sceneName = s.sceneName;
                PlayerManager.Instance.Respawn();
            }
        }
    }
}
