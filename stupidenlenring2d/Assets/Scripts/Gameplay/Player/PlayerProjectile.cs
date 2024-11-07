using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private string damageType;
    [SerializeField] private bool isDOT;
    private List<GameObject> entitiesInTrigger = new();
    private bool isHit;
    private float dottimer;
    public void AddDamage(int damage){
        this.damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D c) {
        if (c.CompareTag("Entity")) {
            entitiesInTrigger.Add(c.gameObject);
            if (!isDOT) {
                if (isHit) {
                c.GetComponent<EntityAttribute>().TakeDamage(damage, 0, damageType, transform);
                isHit = true;
                GetComponent<Collider2D>().isTrigger = false;
                }
            }
        }
    }

    private void Update() {
        if (entitiesInTrigger.Count > 0) {
            dottimer += Time.deltaTime;
            if (isDOT) {
                if (dottimer > 1) {
                    foreach (var entity in entitiesInTrigger)
                    entity.GetComponent<EntityAttribute>().TakeDamage(damage, 0, damageType, transform);
                    dottimer = 0;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D c) {
        if (entitiesInTrigger.Contains(c.gameObject)) {
            entitiesInTrigger.Remove(c.gameObject);
        }
    }
}
