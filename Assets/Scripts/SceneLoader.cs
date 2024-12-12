using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set;}
    public string sceneName { get; private set; }
    public bool hasPlayer { get; private set; }
    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    public void LoadScene(string sceneName, bool hasPlayer = false){
        this.sceneName = sceneName;
        this.hasPlayer = hasPlayer;
        SceneManager.LoadSceneAsync("LoadingScreen");
    }
}
