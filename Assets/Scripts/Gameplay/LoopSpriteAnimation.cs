using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopSpriteAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] frame;
    [SerializeField] private float nextFrameTime;
    [SerializeField] private bool loop;
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool isUI;
    private int index;
    private SpriteRenderer render;
    private Image image;
    private void Awake(){
        if (!isUI)
        render = GetComponent<SpriteRenderer>();
        else
        image = GetComponent<Image>();
    }
    private void Start(){
        if (playOnStart)
        Play();
    }
    private void AnimateObject(){
        if (!isUI)
        render.sprite = frame[index];
        else image.sprite = frame[index];
        index ++;
        if (loop){
        if (index >= frame.Length)
        index = 0;
        }else{ if (index >= frame.Length) CancelInvoke(nameof(AnimateObject));}
    }
    public void Play(){
        index = 0;
        CancelInvoke(nameof(AnimateObject));
        InvokeRepeating(nameof(AnimateObject), 0, nextFrameTime);
    }
    public void SetFrame(Sprite[] frame, bool isLoop = false, bool isUI = false, float frameTime = 0.2f){
        this.frame = frame;
        loop = isLoop;
        this.isUI = isUI;
        nextFrameTime = frameTime;
    }
}
