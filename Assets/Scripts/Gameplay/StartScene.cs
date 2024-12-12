using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;
    private void Start(){
        AudioManager.Instance?.StopBGM();
        if (bgm)
        AudioManager.Instance.PlayBGM(bgm);
    }
}
