using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private void Start(){
        Destroy(gameObject, lifeTime);
    }
    public void SetLifeTime(float lifeTime){
        this.lifeTime = lifeTime;
    }
}
