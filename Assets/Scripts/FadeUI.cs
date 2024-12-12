using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private float lifeTime;
    public bool isFading { get; private set; }
    [SerializeField] private bool fadeOnStart;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start(){
        if (fadeOnStart){
            Fade();
        }
    }
    public void SetValue(float a, float b){
        fadeTime = a;
        lifeTime = b;
    }
    public void Fade(){
        FadeIn(fadeTime);
        Invoke(nameof(end), lifeTime);
    }
    private void end(){
        isFading = false;
        FadeOut(fadeTime);
    }
    public void FadeIn(float duration)
    {
        isFading = true;
        StartCoroutine(FadeCoroutine(0, 1, duration));
    }

    public void FadeOut(float duration)
    {
        if (gameObject.activeInHierarchy)StartCoroutine(FadeCoroutine(1, 0, duration));
    }

    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}
