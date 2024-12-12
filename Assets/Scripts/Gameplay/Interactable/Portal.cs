using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string whereToGo;
    [SerializeField] private string message;
    [SerializeField] private UIConfirmMessage cm;
    [SerializeField] private bool instantPortal;
    private void Start(){
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            if (!instantPortal)
            cm.ShowConfirmMessage(message, Travel);
            else Travel();
        }
    }
    // LoadScene("") là do dòng tiếp theo gán wheretogo vào sceneName rồi lưu game luôn
    private void Travel(){
        SceneLoader.Instance.LoadScene(whereToGo, true);
    }
}
