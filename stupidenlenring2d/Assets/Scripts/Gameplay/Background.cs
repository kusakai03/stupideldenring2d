using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    
    [SerializeField] private float widthScale;
    [SerializeField] private float heightScale;
    [SerializeField] private Camera cam;
    [SerializeField] private bool isSky;
    private void Update(){
        if (cam){
            if (isSky)
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 5);
            GetComponent<SpriteRenderer>().size = new Vector2(widthScale * cam.orthographicSize, heightScale * cam.orthographicSize);
        }
    }
}
