using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource[] sfx;
    private void Awake(){
        if (Instance == null){
        Instance = this;
        DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);
    }
    public void PlayBGM (AudioClip bgm){
        this.bgm.clip = bgm;
        this.bgm.Play();
    }
    public void StopBGM(){
        this.bgm.Stop();
    }
}
