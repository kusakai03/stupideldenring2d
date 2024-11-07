using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private UIConfirmMessage confirmBox;
    [SerializeField] private string confirmMessage;
    private bool hasClick;
    private void Start(){
        SaveLoad.Instance.onAuthenticated += OnAuthenticated;
        timeline.Play();
        Invoke(nameof(Till),6);
    }
    private void OnDisable(){
        SaveLoad.Instance.onAuthenticated -= OnAuthenticated;
    }

    private void OnAuthenticated(object sender, EventArgs e)
    {
        timeline.Resume();
    }

    private void Till(){
        timeline.Pause();
    }
    public void TapToStart(){
        if (!SaveLoad.Instance.signedin)
        SaveLoad.Instance.Initialize();
        else timeline.Resume();
    }
    public void NewGameButton(){
        confirmBox.ShowConfirmMessage(confirmMessage,ConfirmNewGame);
    }
    private void ConfirmNewGame(){
        SceneLoader.Instance.LoadScene("NewGameScene");
        SaveLoad.Instance.newGame = true;
    }
    public async void Continue(){
        if (!hasClick){
            hasClick = true;
            bool hasSaved = await SaveLoad.Instance.KeyExists(SaveLoad.Instance.saveKey);
            if (hasSaved){
                SceneLoader.Instance.LoadScene("");
            }
            else{
                hasClick = false;
            }
        }
    }
    public void Setting(){

    }
    public void ExitGame(){}
}
