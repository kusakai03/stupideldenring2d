using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundProjectileSpawn : MonoBehaviour
{
    private bool hitGround;
    private GameObject objectToSpawn;
    private int damage;

    public void SetObject(GameObject objectToSpawn, int damage){
        this.objectToSpawn = objectToSpawn;
        this.damage = damage;
    }
    private void OnTriggerStay2D (Collider2D other){
        if (other.tag == "Ground"){
            if (!hitGround){
                transform.position += 50 * Time.deltaTime * Vector3.up;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Ground"){
            if (!hitGround){
                hitGround = true;
                PlayerProjectile p = Instantiate(objectToSpawn, transform.position, Quaternion.identity).GetComponent<PlayerProjectile>();
                p.AddDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
    public void OnEnable(){
        hitGround = false;
    }
}
