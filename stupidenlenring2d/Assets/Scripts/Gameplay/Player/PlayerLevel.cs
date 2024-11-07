using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int STR { get; private set; }
    public int STRxp { get; private set; }
    public int STRxpcap { get; private set; }
    public int INT { get; private set; }
    public int INTxp { get; private set; }
    public int INTxpcap { get; private set; }
    public int DEX { get; private set; }
    public int DEXxp { get; private set; }
    public int DEXxpcap { get; private set; }
    public int VIT  { get; private set; }
    public int VITxp { get; private set; }
    public int VITxpcap { get; private set; }
    public event EventHandler OnPlayerUpgrade;
    public void SetSTRxpcap(){
        STRxpcap = STR * 2 * STR * 50;
    }
    public void STRLVup(){
        if (STRxp >= STRxpcap){
            STRxp -= STRxpcap;
            STR++;
            SetSTRxpcap();
            OnPlayerUpgrade?.Invoke(this, EventArgs.Empty);
        }
    }
    public void GainSTRxp (int xp){
        STRxp += xp; 
        STRLVup();
    }
    public void SetINTxpcap(){
        INTxpcap = INT * 2 * INT * 60;
    }
    public void INTLVup(){
        if (INTxp >= INTxpcap){
            INTxp -= INTxpcap;
            INT++;
            SetINTxpcap();
            OnPlayerUpgrade?.Invoke(this, EventArgs.Empty);
        }
    }
    public void GainINTxp (int xp){
        INTxp += xp; 
        INTLVup();
    }
    public void SetDEXxpcap(){
        DEXxpcap = DEX * 2 * DEX * 20;
    }
    public void DEXLVup(){
        if (DEXxp >= DEXxpcap){
            DEXxp -= DEXxpcap;
            DEX++;
            SetDEXxpcap();
            OnPlayerUpgrade?.Invoke(this, EventArgs.Empty);
        }
    }
    public void GainDEXxp (int xp){
        DEXxp += xp; 
        DEXLVup();
    }
    public void SetVITxpcap(){
        VITxpcap = VIT * 2 * VIT * 30;
    }
    public void VITLVup(){
        if (VITxp >= VITxpcap){
            VITxp -= VITxpcap;
            VIT++;
            SetVITxpcap();
            OnPlayerUpgrade?.Invoke(this, EventArgs.Empty);
        }
    }
    public void GainVITxp (int xp){
        VITxp += xp; 
        VITLVup();
    }
    public int GetBaseATKStat(int strength, int dexerity){
        return (int)(5 + STR * (0.96f + strength * 0.0098f) + DEX * (0.24f + dexerity * 0.0024f));
    }
    public int GetBaseMAGStat(int intelligence){
        return (int)(1 + INT * (3.12f + intelligence * 0.032f));
    }
    public int GetBaseHPStat(int vitality){
        return (int)(1000 + VIT * (9.24f + vitality * 0.092f));
    }
    public int GetBaseStaminaStat(int dexerity){
        return (int)(200 + DEX * (0.3f + dexerity * 0.003f));
    }
    public int GetBaseMPStat(int intelligence){
        return (int)(50 + INT * (1 + intelligence * 0.01f));
    }
    public float GetMoveSpeed(int dexerity){
        return 3 + DEX * (0.002f + dexerity * 0.0005f);
    }
    public int GetDmgResStat(int vitality){
        return (int)(VIT * (2 + vitality * 0.002f));
    }
    private void Start(){
        SetDEXxpcap();
        SetINTxpcap();
        SetSTRxpcap();
        SetVITxpcap();
    }
}
