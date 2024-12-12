using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIVolumeSetting : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider; 
    [SerializeField] private TextMeshProUGUI vlm1;
    [SerializeField] private TextMeshProUGUI vlm2;
    [SerializeField] private TextMeshProUGUI vlm3;
    GameSetting g;
    private void OnEnable(){
        g = GameSetting.Instance;
        masterSlider.value = g.masterVolume;
        bgmSlider.value = g.bgmVolume;
        sfxSlider.value = g.sfxVolume;
        OnChangeValue();
    }
    private void OnDisable(){
        g.OnVolumeChange(masterSlider.value, bgmSlider.value, sfxSlider.value);
    }
    public void OnChangeValue(){
        vlm1.text = ((int)(masterSlider.value*100)).ToString();
        vlm2.text = ((int)(bgmSlider.value*100)).ToString();
        vlm3.text = ((int)(sfxSlider.value*100)).ToString();
    }
}
