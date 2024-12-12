using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttribute : MonoBehaviour
{
    public event EventHandler onGettingHit;
    public event EventHandler onOutOfPoise;
    public event EventHandler onOutOfHealth;
    public static event EventHandler onOutOfHealthStatic;
    public string bossName;
    [SerializeField] private float damage;
    [SerializeField] private float strength;
    [SerializeField] private float hp;
    [SerializeField] private float def;
    [SerializeField] private float finalPoise;
    [SerializeField] private int finalPhysicalRes;
    [SerializeField] private int finalFireRes;
    [SerializeField] private int finalIceRes;
    [SerializeField] private int finalLightningRes;
    [SerializeField] private int finalMagicRes;
    private float currenthp;
    private float currentPoise;
    private Hitbox hitbox;
    [SerializeField] private bool isMelee;
    [SerializeField] private bool isTough;
    [SerializeField] private int dropRate;
    [SerializeField] private GameObject dropItem;
    [SerializeField] private List<ItemDrop> itemsToDrop;
    public GameObject hitfrom { get; private set; }
    private void Awake(){
        if (isMelee)
        hitbox = GetComponentInChildren<Hitbox>();
    }
    private void Start(){
        currentPoise = finalPoise;
        currenthp = hp;
        if (isMelee)
        hitbox.onBeingParried += OnBeingParried;
    }
    
    public void ShotSomething(GameObject theThing, float damageMultiplier, string damageType, Vector2 direction, float fireForce){
        Hitbox hb = Instantiate(theThing, transform.position, Quaternion.identity).GetComponent<Hitbox>();
        hb.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireForce, ForceMode2D.Impulse);
        hb.GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        hb.SetObjectDamage(damage * damageMultiplier, strength, damageType);
    }

    public float HPPercent(){
        return (currenthp / hp) * 100;
    }
    public float GetHp(){
        return hp;
    }
    public float GetCurrentHP(){
        return currenthp;
    }
    private void OnBeingParried(object sender, EventArgs e)
    {
        if (currentPoise > 0)
        TakeDamage(0,finalPoise+1, "Physical", transform);
    }

    public void SetAttackDamage(float multiplier, string damageType){
        if (isMelee){
            float hitStrength = multiplier == 0? 0: strength;
            hitbox.SetObjectDamage(damage * multiplier, hitStrength, damageType);           
        }
    }
    public void TakeDamage(float damage, float poise, string damageType, Transform objecthit){ //Damage type: Physical, Fire, Ice, Lightning, Magic
        float finalDamage;
        float damageResistance = def * 8; //Tính giảm thương dựa theo phòng thủ
        damageResistance = damageResistance > 80? 80 : damageResistance;
        finalDamage = damage - GetNumberByPercent(damage, damageResistance); //Tổng kết sát thương sẽ nhận được
        switch (damageType){
            case "Physical":
                finalDamage -= GetNumberByPercent(finalDamage, finalPhysicalRes);
            break;
            case "Fire":
                finalDamage -= GetNumberByPercent(finalDamage, finalFireRes);
            break;
            case "Ice":
                finalDamage -= GetNumberByPercent(finalDamage, finalIceRes);
            break;
            case "Lightning":
                finalDamage -= GetNumberByPercent(finalDamage, finalLightningRes);
            break;
            case "Magic":
                finalDamage -= GetNumberByPercent(finalDamage, finalMagicRes);
            break;
        }
        currenthp -= finalDamage;
        hitfrom = objecthit.gameObject;
        currentPoise -= poise; //Trừ sức bền khi bị đánh
        if (currentPoise < 0){
            currentPoise = 0;
            Vector2 dir = new Vector2(transform.position.x - objecthit.position.x, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, 1).normalized * 5;
            onOutOfPoise?.Invoke(this, EventArgs.Empty);
        } 
        else{
            if (!isTough)
            onGettingHit?.Invoke(this, EventArgs.Empty);
        }
        if (currenthp <= 0){
            if (dropRate >= UnityEngine.Random.Range(1, 100) && itemsToDrop.Count > 0){
                ItemsObtain i = Instantiate(dropItem, transform.position, Quaternion.identity)
                .GetComponent<ItemsObtain>();
                i.SetItems(itemsToDrop);
            }
            onOutOfHealth?.Invoke(this, EventArgs.Empty);
            onOutOfHealthStatic?.Invoke(this, EventArgs.Empty);
        }
        if (finalDamage !=0)
        GameSetting.Instance.ShowFloatingDamage((int)finalDamage,damageType,transform.position);
    }
    private float GetNumberByPercent(float totalNum, float percent){
        return totalNum * percent / 100;
    }
    public void WakeFromFaint(){
        currentPoise = finalPoise;
    }
}
