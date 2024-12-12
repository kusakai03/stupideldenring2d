using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterracting : MonoBehaviour
{
    private DialogueLines lines;
    [SerializeField] private GameObject dialogueBox;
    public void SetValue(DialogueLines lines){
        this.lines = lines;
    }
    private void Update(){
        if (lines)
        if (!lines.player){
            lines = null;
            gameObject.SetActive(false);
        }
    }
    public void OnClick(){
        dialogueBox.SetActive(true);
        dialogueBox.GetComponent<UIDialogBox>().SetValue(lines.story);
    }
}
