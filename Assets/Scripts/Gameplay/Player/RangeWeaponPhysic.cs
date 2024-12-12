using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponPhysic : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Sprite[] holdingAnimation;
    [SerializeField] private Sprite[] shootAnimation;
    private LoopSpriteAnimation anim;
    [SerializeField] private Transform firepoint;
    private float maxForce;
    public float force;
    private Vector2 dir;
    private PlayerMoving master;
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<LoopSpriteAnimation>();
    }
    private void Start(){
        PlayerManager.Instance.onHoldingArrow += OnHoldingArrow;
        PlayerManager.Instance.onShootingArrow += OnShootingArrow;
    }
    private void OnDisable(){
        PlayerManager.Instance.onHoldingArrow -= OnHoldingArrow;
        PlayerManager.Instance.onShootingArrow -= OnShootingArrow;
    }

    private void OnShootingArrow(object sender, EventArgs e)
    {
        anim.SetFrame(shootAnimation);
        anim.Play();
    }

    private void OnHoldingArrow(object sender, EventArgs e)
    {
        anim.SetFrame(holdingAnimation);
        anim.Play();
    }

    public void SetMaster(PlayerMoving master){
        this.master = master;
    }

    private void Update(){
        transform.position = master.transform.position;
    }
    public Transform Firepoint(){
        return firepoint;
    }  
    public void FireArrow(GameObject arrow, int damage){
        dir = GameSetting.Instance.joystickDir;
        force = dir.magnitude * maxForce;
        int finalDamage = (int)(damage * (1 + (force / maxForce)));
        Rigidbody2D rb = Instantiate(arrow, firepoint.position, firepoint.rotation).GetComponent<Rigidbody2D>();
        rb.GetComponent<PlayerArrowPhysic>().SetDamage(finalDamage);
        rb.AddForce(dir.normalized * force, ForceMode2D.Impulse);
    }
    public void SetMaxforce(float maxforce){
        this.maxForce = maxforce;
    }
}
