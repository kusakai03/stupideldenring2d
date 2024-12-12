using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnStart : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private float loadtime;
    void Start()
    {
        Invoke(nameof(L),loadtime);
    }
    private void L(){
        SceneManager.LoadSceneAsync(sceneName);
    }
}
