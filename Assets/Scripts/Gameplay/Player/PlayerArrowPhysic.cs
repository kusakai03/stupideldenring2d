using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowPhysic : MonoBehaviour
{
    [SerializeField] private float fireForce;
    [SerializeField] private float dmgMultiplier;
    [SerializeField] private string dmgElement;
    [SerializeField] private float minusAngle;
    private Rigidbody2D rb;
    public List<GameObject> objectHits;
    private int damage;
    private bool isHit = false;
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update(){
        if (!isHit)
        rb.rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - minusAngle;
    }
    public void SetDamage(int damage){
        this.damage = (int)(damage * dmgMultiplier);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Entity"){
            if (other.GetComponent<EnemyState>().State != EnemyState.EntityState.Dead){
                other.GetComponent<EntityAttribute>().TakeDamage(damage, 10, dmgElement, transform);
                GetComponent<Collider2D>().isTrigger = false;
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                isHit = true;
            }
        }
        if (other.tag == "Ground" || other.tag == "Wall"){
            GetComponent<Collider2D>().isTrigger = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            isHit = true;
        }
    }
}
