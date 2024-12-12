using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    [Header ("Equipped Weapon Attribute")]
    public float dmgMultiplier;
    public int normalAtkStaminaCost;
    public int chargeAtkStaminaCost;
    [Header ("Current Equipped")]
    public EquipmentData helmet;
    public EquipmentData armor;
    public EquipmentData glove;
    public EquipmentData leg;
    public EquipmentData weapon1;
    public EquipmentData weapon2;
    public EquipmentData ring1;
    public EquipmentData ring2;
    public EquipmentData ring3;
    public EquipmentData ring4;
    public ItemOwned arrow;
    //Equipment Base Attribute
    public int strength { get; private set; }
    public int intelligence { get; private set; }
    public int dexerity {get; private set;}
    public int vitality { get; private set; }
    //Current ingame attribute
    public float currentHP {get; private set;}
    public float currentMP {get; private set;}
    public float currentStamina {get; private set;}
    public float currentPoise {get; private set;}
    public int currentpoison {get; private set;}
    public int currentburn {get; private set;}
    public int currentfrozen {get; private set;}
    public int currentbleed {get; private set;}
    public int currentblind {get; private set;}
    //Player Final Atrribute
    public float finalATK {get; private set;}
    public float finalMAG {get; private set;}
    public float finalHP {get; private set;}
    public float finalMP {get; private set;}
    public float finalStamina {get; private set;}
    public float finalPoise {get; private set;}
    public int finalPhysicalRes {get; private set;}
    public int finalFireRes {get; private set;}
    public int finalIceRes {get; private set;}
    public int finalLightningRes {get; private set;}
    public int finalMagicRes {get; private set;}
    public int physicalBlock {get; private set;}
    public int fireblock {get; private set;}
    public int iceblock {get; private set;}
    public int lightningblock {get; private set;}
    public int magicblock {get; private set;}
    public int poison {get; private set;}
    public int burn {get; private set;}
    public int frozen {get; private set;}
    public int bleed {get; private set;}
    public int blind {get; private set;}
    public int healingBonus;
    public float staminaRegen;
    [Header ("Script references")]
    private PlayerMoving playerMoving;
    public GameObject currentWeapon {get; private set;}
    [SerializeField] private GameObject ringHolder;
    private float lastDamageDeal;
    public int lastDamageTaken;
    public PlayerLevel lv {get; private set;}
    public PlayerBuffEffect effect {get; private set;}
    //Events
    public event EventHandler onOutOfHealth;
    public event EventHandler onOutOfPoise;
    public event EventHandler onGettingHit;
    public event EventHandler onWeaponUpdated;
    public event EventHandler onAttributeUpdated;
    public event EventHandler onDamageTakenCalculated;
    private void OnEnable(){
        playerMoving.onNormalAttack += NormalAttack;
        playerMoving.onChargeAttack += ChargeAttack;
        playerMoving.onDashing += Dashing;
        lv.OnPlayerUpgrade += PlayerUpgrade;
        PlayerManager.Instance.onChangeEquipment += UpdateStatus;
        PlayerManager.Instance.onHoldingArrow += HoldingArrow;
        PlayerManager.Instance.onShootingArrow += ShootingArrow;
        UpdateAttribute(true);
        FindArrow();
        UIGameplayBar.Instance?.SetMaxValue(finalHP, finalMP, finalStamina);
        Campfire();
    }

    private void FindArrow()
    {
        foreach (var i in ItemManager.ins.itemOwneds){
            if (ItemManager.ins.GetItemByid(i.itemID).itemType == "arrow"){
                arrow = i;
            }
        }
    }

    private void OnDisable(){
        playerMoving.onNormalAttack -= NormalAttack;
        playerMoving.onChargeAttack -= ChargeAttack;
        playerMoving.onDashing -= Dashing;
        lv.OnPlayerUpgrade -= PlayerUpgrade;
        PlayerManager.Instance.onChangeEquipment -= UpdateStatus;
        PlayerManager.Instance.onHoldingArrow -= HoldingArrow;
        PlayerManager.Instance.onShootingArrow -= ShootingArrow;
    }

    private void ShootingArrow(object sender, EventArgs e)
    {
        RangeWeaponPhysic rp = currentWeapon.GetComponent<RangeWeaponPhysic>();
        int sta = (int)rp.force;
        if (currentStamina >= sta){
            currentStamina -= sta;
            rp.FireArrow(ItemManager.ins.GetItemByid(arrow.itemID)?.consumeItem, (int)(finalATK * dmgMultiplier));
        }
    }

    private void HoldingArrow(object sender, EventArgs e)
    {
        if (arrow.quantity > 0)
        currentWeapon.GetComponent<RangeWeaponPhysic>().SetMaxforce(chargeAtkStaminaCost);
        else 
        UIFloatingMessage.Instance.ShowMessage("Không có mũi tên nào");
    }

    private void UpdateStatus(object sender, EventArgs e)
    {
        UpdateAttribute(true);
    }

    private void PlayerUpgrade(object sender, EventArgs e)
    {
        UpdateAttribute(false);
        PlayerManager.Instance.ShowLevelupMessage();
    }

    private void Update(){
        UIGameplayBar.Instance?.SetCurrentValue(currentHP, currentMP, currentStamina);
    }
    private void FixedUpdate(){
        if (currentStamina < finalStamina && playerMoving.state == PlayerMoving.playerState.Idle){
            currentStamina += GetNumberByPercent(finalStamina, staminaRegen) * Time.fixedDeltaTime;
        }
        if (playerMoving.state == PlayerMoving.playerState.Idle)
            if (currentPoise < finalPoise){
                currentPoise += 50 * Time.fixedDeltaTime;
            }
    }
    public void LastDamageDeal(float damage){
        lastDamageDeal = damage;
        lv.GainSTRxp((int)damage);
    }
    private void SetAttribute(){
        healingBonus = 100;
        finalPoise = 50;
        finalHP = lv.GetBaseHPStat(vitality);
        finalMP = lv.GetBaseMPStat(intelligence);
        finalATK = lv.GetBaseATKStat(strength, dexerity);
        finalStamina = lv.GetBaseStaminaStat(dexerity);
        finalMAG = lv.GetBaseMAGStat(intelligence);
    }
    public void Campfire(){
        currentHP = finalHP;
        currentMP = finalMP;
        currentStamina = finalStamina;
        currentPoise = finalPoise;
        currentpoison = 0;
        currentbleed = 0;
        currentblind = 0;
        currentburn = 0;
        currentfrozen = 0;
    }
    public bool NatkStamina(){
        return currentStamina >= normalAtkStaminaCost;
    }
    public bool SatkStamina(){
        return currentStamina >= chargeAtkStaminaCost;
    }
    public bool DashStamina(){
        return currentStamina >= 50;
    }
    
    private void Dashing(object sender, EventArgs e)
    {
        lv.GainDEXxp(100);
        currentStamina -= 50;
    }
    public void SpendMP(int MP){
        currentMP -= MP;
    }
    private void ChargeAttack(object sender, EventArgs e)
    {
        if (PlayerManager.Instance.GetEquipmentByID(weapon1.eid).weapon.weaponType != "Ranged"){
            currentStamina -= chargeAtkStaminaCost;
            currentWeapon?.GetComponent<WeaponPhysic>().SetDamage(finalATK * (dmgMultiplier + 0.5f + playerMoving.GetChargeTime()), chargeAtkStaminaCost);
            if (effect.effects.Any(a => a.effectType == "Bleed")){
                TakeDamage(GetNumberByPercent(finalHP, 5), 0, "", transform);
            }
        }
    }

    private void NormalAttack(object sender, EventArgs e)
    {
        if (PlayerManager.Instance.GetEquipmentByID(weapon1.eid).weapon.weaponType != "Ranged"){
            currentStamina -= normalAtkStaminaCost;
            currentWeapon?.GetComponent<WeaponPhysic>().SetDamage(finalATK * dmgMultiplier, normalAtkStaminaCost);
            if (effect.effects.Any(a => a.effectType == "Bleed")){
                TakeDamage(GetNumberByPercent(finalHP, 5), 0, "", transform);
            }
        }
    }

    private void Awake(){
        playerMoving = GetComponent<PlayerMoving>();
        lv = GetComponent<PlayerLevel>();
        effect = GetComponent<PlayerBuffEffect>();
    }
    public void Equipment(EquipmentData equipment, string type){
        PlayerManager p = PlayerManager.Instance;
        Equipment e = p.GetEquipmentByID(equipment.eid);
        if (GetEquipmentSlotByType(type) != null){
            CancelEquip(type);
        }
        if (type.Contains("ring")){
            if (ring1 == equipment)
            ring1 = null;
            else if (ring2 == equipment)
            ring2 = null;
            else if (ring3 == equipment)
            ring3 = null;
            else if (ring4 == equipment)
            ring4 = null;
        }
        switch (type){
            case "helmet":
            helmet = equipment;
            break;
            case "armor":
            armor = equipment;
            break;
            case "glove":
            glove = equipment;
            break;
            case "leg":
            leg = equipment;
            break;
            case "weapon1":
            weapon1 = equipment;
            break;
            case "weapon2":
            weapon2 = equipment;
            break;
            case "ring1":
            ring1 = equipment;
            break;
            case "ring2":
            ring2 = equipment;
            break;
            case "ring3":
            ring3 = equipment;
            break;
            case "ring4":
            ring4 = equipment;
            break;
        }
    }
    public void CancelEquip(string type){
        switch (type){
            case "helmet":
            helmet = null;
            break;
            case "armor":
            armor = null;
            break;
            case "glove":
            glove = null;
            break;
            case "leg":
            leg = null;
            break;
            case "weapon1":
            weapon1 = null;
            break;
            case "weapon2":
            weapon2 = null;
            break;
            case "ring1":
            ring1 = null;
            break;
            case "ring2":
            ring2 = null;
            break;
            case "ring3":
            ring3 = null;
            break;
            case "ring4":
            ring4 = null;
            break;
        }
    }
    public EquipmentData GetEquipmentSlotByType(string type){
        switch (type){
            case "helmet":
            return helmet;

            case "armor":
            return armor;
            
            case "glove":
            return glove;

            case "leg":
            return leg;
            
            case "weapon1":
            return weapon1;

            case "weapon2":
            return weapon2;

            case "ring1":
            return ring1;

            case "ring2":
            return ring2;

            case "ring3":
            return ring3;

            case "ring4":
            return ring4;
        }
        return default;
    }
    private void UpdateAttribute(bool passiveReset){
        SetDefaultVariables();
        GetAttributeFromEquipment(helmet);
        GetAttributeFromEquipment(armor);
        GetAttributeFromEquipment(glove);
        GetAttributeFromEquipment(leg);
        GetAttributeFromEquipment(weapon1);
        SetAttribute();
        finalPhysicalRes = lv.GetDmgResStat(vitality);
        finalFireRes = lv.GetDmgResStat(vitality);
        finalIceRes = lv.GetDmgResStat(vitality);
        finalLightningRes = lv.GetDmgResStat(vitality);
        finalMagicRes = lv.GetDmgResStat(vitality);
        poison = 100;
        blind = 100;
        bleed = 100;
        frozen = 100;
        burn = 100;
        GetEquipmentRes(helmet);
        GetEquipmentRes(armor);
        GetEquipmentRes(glove);
        GetEquipmentRes(leg);
        GetEquipmentRes(weapon1);
        if (passiveReset)
        ActiveRings();
        onAttributeUpdated?.Invoke(this, EventArgs.Empty);
    }
    private void GetAttributeFromEquipment(EquipmentData equipment){
        PlayerManager p = PlayerManager.Instance;
        if (!p.GetEquipmentByID(equipment?.eid)) return;
        Equipment e = p.GetEquipmentByID(equipment.eid);
        strength += e.strength + equipment.level * e.strup;
        dexerity += e.dexerity + equipment.level * e.dexup;
        intelligence += e.intelligence + equipment.level * e.intup;
        vitality += e.vitality + equipment.level * e.vitup;       
    }
    private void GetEquipmentRes(EquipmentData equipment){
        PlayerManager p = PlayerManager.Instance;
        if (!p.GetEquipmentByID(equipment?.eid)) return;
        Equipment e = p.GetEquipmentByID(equipment.eid);
        finalPoise += e.poise + equipment.level * e.poiseup;
        finalPhysicalRes += e.physicalRES;
        finalFireRes += e.fireRES;
        finalIceRes += e.iceRES;
        finalLightningRes += e.lightningRES;
        finalMagicRes += e.magicRES;
        poison += e.poison;
        burn += e.burn;
        frozen += e.frozen;
        bleed += e.bleed;
        blind += e.blind;
    }
    private void SetDefaultVariables(){
        strength = 0;
        vitality = 0;
        dexerity = 0;
        intelligence = 0;
        finalATK = 0;
        finalMAG = 0;
        finalPoise = 0;
        finalPhysicalRes = 0;
        finalFireRes = 0;
        finalIceRes = 0;
        finalLightningRes = 0;
        finalMagicRes = 0;
        poison = 0;
        burn = 0;
        frozen = 0;
        bleed = 0;
        blind = 0;
    }
    public void UpdateWeapon(WeaponData weapon, GameObject weaponObject){
        if (weapon){
            dmgMultiplier = weapon.dmgMultiplier;
            physicalBlock = weapon.physicalRes;
            fireblock = weapon.fireRes;
            iceblock = weapon.iceRes;
            lightningblock = weapon.lightningRes;
            magicblock = weapon.magicRes;
            currentWeapon = weaponObject;
            normalAtkStaminaCost = weapon.normalAttackStamina;
            chargeAtkStaminaCost = weapon.chargeAttackStamina;
            SetAttribute();
            if (weapon.weaponSkill)
            GetComponentInChildren<PlayerSkillHolder>().ReplaceSkill(0, new SkillData(weapon.weaponSkill.skillID, PlayerManager.Instance.ownedEquipments.Find(x => x.eid == weapon.equipment.eid).level, weapon.weaponSkill.manaCost));
            onWeaponUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
    public void OnWeaponUnequipped(){
        if (currentWeapon){
            currentWeapon = null;
        }
    }

    public void SwitchWeapon(){
        if (weapon2 != null && playerMoving.state == PlayerMoving.playerState.Idle){
            EquipmentData temp;
            temp = weapon1;
            weapon1 = weapon2;
            weapon2 = temp;
            PlayerManager.Instance.ChangeEquipment();
            string message = PlayerManager.Instance.GetEquipmentByID(weapon1.eid)? "Trang bị " + PlayerManager.Instance.GetEquipmentByID(weapon1.eid).ename : "Bạn không thể đánh bằng tay không";
            UIFloatingMessage.Instance.ShowMessage(message);
        }
    }

    private void ActiveRings(){
        foreach(Transform child in ringHolder.transform){
            Destroy(child.gameObject);
        }
        PlayerManager p = PlayerManager.Instance;
        if (p.GetEquipmentByID(ring1.eid))
        Instantiate(p.GetEquipmentByID(ring1?.eid).passive, ringHolder.transform);
        if (p.GetEquipmentByID(ring2.eid))
        Instantiate(p.GetEquipmentByID(ring2?.eid).passive, ringHolder.transform);
        if (p.GetEquipmentByID(ring3.eid))
        Instantiate(p.GetEquipmentByID(ring3?.eid).passive, ringHolder.transform);
        if (p.GetEquipmentByID(ring4.eid))
        Instantiate(p.GetEquipmentByID(ring4?.eid).passive, ringHolder.transform);
    }
    public void buffATK(int atk){
        finalATK += atk;
    }
    public void TakeDamage(float damage, float poise, string damageType, Transform objecthit){ //Damage type: Physical, Fire, Ice, Lightning, Magic
        if (playerMoving.state != PlayerMoving.playerState.Evade && playerMoving.CanHitPlayer()){
            float finalDamage = 0;
            switch (damageType){//Tổng kết sát thương sẽ nhận được dựa theo loại sát thương
                case "Physical":
                    finalDamage = damage - GetNumberByPercent(damage, finalPhysicalRes/10);
                break;
                case "Fire":
                    finalDamage = damage - GetNumberByPercent(damage, finalFireRes/10);
                break;
                case "Ice":
                    finalDamage = damage - GetNumberByPercent(damage, finalIceRes/10);
                break;
                case "Lightning":
                    finalDamage = damage - GetNumberByPercent(damage, finalLightningRes/10);
                break;
                case "Magic":
                    finalDamage = damage - GetNumberByPercent(damage, finalMagicRes/10);
                break;
                case "":
                    finalDamage = damage;
                break;
                }           
            if (playerMoving.state == PlayerMoving.playerState.Block && objecthit){ //Nếu đang đỡ đòn thì giảm thêm sát thương
                switch (damageType){
                    case "Physical":
                        finalDamage -= GetNumberByPercent(finalDamage, physicalBlock);
                    break;
                    case "Fire":
                        finalDamage -= GetNumberByPercent(finalDamage, fireblock);
                    break;
                    case "Ice":
                        finalDamage -= GetNumberByPercent(finalDamage, iceblock);
                    break;
                    case "Lightning":
                        finalDamage -= GetNumberByPercent(finalDamage, lightningblock);
                    break;
                    case "Magic":
                        finalDamage -= GetNumberByPercent(finalDamage, magicblock);
                    break;
                }
            }
            lastDamageTaken = (int)finalDamage;
            onDamageTakenCalculated?.Invoke(this, EventArgs.Empty);
            currentHP -= lastDamageTaken;
            lv.GainVITxp(lastDamageTaken);
            onGettingHit?.Invoke(this, EventArgs.Empty);
            currentPoise -= poise; //Trừ sức bền khi bị đánh
            if (currentPoise < 0){ //Nếu hết sức bền thì dùng thể lực thay thế
                currentStamina += currentPoise*2;
                currentPoise = 0;
                if (currentStamina <= 0){ //Nếu cả poise với thể lực đều dưới 0
                    if (objecthit){
                        Vector2 dir = new Vector2(transform.position.x - objecthit.position.x, 0);
                        if (playerMoving.state != PlayerMoving.playerState.Daze)
                        GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, 2).normalized * poise/5;
                    }
                    onOutOfPoise?.Invoke(this, EventArgs.Empty); //Nhân vật bị choáng
                    currentStamina = 0;
                }
            } 
            if (currentHP <= 0){
                onOutOfHealth?.Invoke(this, EventArgs.Empty);
            }
            if (lastDamageTaken != 0)
            GameSetting.Instance.ShowFloatingDamage(lastDamageTaken, damageType, transform.position);
        }
    }
    public void ApplyStatusEffect(string effectType, int number){
        switch (effectType){
            case "Poison":
            currentpoison += number;
            break;
            case "Blind":
            currentblind += number;
            break;
            case "Bleed":
            currentbleed += number;
            break;
            case "Burn":
            currentburn += number;
            break;
            case "Frozen":
            currentfrozen += number;
            break;
        }
        CheckingStatus();
    }
    private void CheckingStatus(){
        if (currentpoison >= poison){
            currentpoison = 0;
            effect.AddEffect("Poison", GetNumberByPercent(finalHP, 1), 300);
        }
        if (currentblind >= blind){
            currentblind = 0;
            effect.AddEffect("Blind", 0, 10);
        }
        if (currentbleed >= bleed){
            currentbleed = 0;
            effect.AddEffect("Bleed", 0, 100);
        }
        if (currentburn >= burn){
            currentburn = 0;
            effect.AddEffect("Burn", GetNumberByPercent(finalHP, 5), 20);
        }
        if (currentfrozen >= frozen){
            currentfrozen = 0;
            effect.AddEffect("Frozen", GetNumberByPercent(finalHP, 40), 1);
        }
    }
    public void Healing(int heal){
        currentHP += (heal * healingBonus /100);
    }
    public float GetNumberByPercent(float totalNum, float percent){
        return totalNum * percent / 100;
    }
    public void FallDamage(float flyingTime){
        TakeDamage(GetNumberByPercent(finalHP, flyingTime*10), flyingTime*10, "Physical", transform);
    }
}
