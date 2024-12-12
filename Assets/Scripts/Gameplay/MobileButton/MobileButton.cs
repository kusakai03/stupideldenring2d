using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MobileButton : MonoBehaviour
{
    public static MobileButton Instance { get; private set;}
    public static event EventHandler onJumpButton;
    private PlayerMoving player;
    private PlayerAttribute playerAttribute;
    [SerializeField] private GameObject obtainItemButton;
    [SerializeField] private GameObject interractButton;
    [SerializeField] private GameObject campfireButton;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private GameObject moveupButton;
    [SerializeField] private GameObject movedownButton;
    [SerializeField] private UISkillSlotButton[] skillSlots; 
    [SerializeField] private GameObject bottomRightButtons;
    [SerializeField] private UIJoystick rangeAttackButton;
    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
    }
    private async void Start() {
        await WaitForPlayer();
        player = PlayerManager.Instance.currentPlayer.GetComponent<PlayerMoving>();
        playerAttribute = player.GetComponent<PlayerAttribute>();
        playerAttribute.onWeaponUpdated += OnWeaponUpdated;
        rangeAttackButton.onHoldingJoystick += OnHoldingRangeButton;
        rangeAttackButton.onReleasingJoystick += OnReleasingRangeButton;
        player.GetComponentInChildren<PlayerSkillHolder>().OnSkillChanged += OnSkillChanged;
        PlayerManager.Instance.onSpawnSuccess += OnSpawnSuccess;
    }

    private async Task WaitForPlayer() {
        while (PlayerManager.Instance.currentPlayer == null) {
            await Task.Yield();  
        }
    }
    private void OnDisable(){
        playerAttribute.onWeaponUpdated -= OnWeaponUpdated;
        if (player)
        player.GetComponentInChildren<PlayerSkillHolder>().OnSkillChanged -= OnSkillChanged;
        rangeAttackButton.onHoldingJoystick -= OnHoldingRangeButton;
        rangeAttackButton.onReleasingJoystick -= OnReleasingRangeButton;
    }

    private void OnReleasingRangeButton(object sender, EventArgs e)
    {
        PlayerManager.Instance.ShootingArrow();
    }

    private void OnHoldingRangeButton(object sender, EventArgs e)
    {
        PlayerManager.Instance.HoldingArrow();
    }

    private void OnSkillChanged(object sender, EventArgs e)
    {
        UpdateSkill();
    }

    private void Update() {
        if (player){
            moveupButton.SetActive(player.isLadder);
            movedownButton.SetActive(player.isLadder);
        }
    }
    private void OnWeaponUpdated(object sender, EventArgs e)
    { 
        Equipment eq = PlayerManager.Instance.GetEquipmentByID(player.GetComponent<PlayerAttribute>().weapon1.eid);
        bottomRightButtons.SetActive(eq.weapon.weaponType != "Ranged");
        rangeAttackButton.gameObject.SetActive(eq.weapon.weaponType == "Ranged");
    }
    private void OnSpawnSuccess(object sender, EventArgs e)
    {
        UpdateSkill();
    }
    public void ShowObtainButton(ItemsObtain chest){
        obtainItemButton.SetActive(true);
        obtainItemButton.GetComponent<UIObtainItemButton>().SetValue(chest);
    }
    public void JumpButton(){
        onJumpButton?.Invoke(this, EventArgs.Empty);
    }
    public void ShowDialogueButton(DialogueLines lines){
        interractButton.SetActive(true);
        interractButton.GetComponent<UIInterracting>().SetValue(lines);
    }
    public void ShowCampfireButton(Campfire campfire){
        campfireButton.GetComponent<UICampfireButton>().SetValue(campfire);
    }
    public void AttackButton(){ //Nhấp vào nút để đánh thường
        player.NormalAttack();
    }
    public void HoldingAttackButton(){ //Giữ nút đánh thường
        player.StartChargeAttack();
    }
    public void ReleasingAttackButton(){
        player.ReleaseChargeAttack();
    }
    public void SwitchWeaponButton(){
        player.GetComponent<PlayerAttribute>().SwitchWeapon();
    }
    public void EvadeButton(){
        player.EvadeMove();
    }
    public void HoldingBlock(){
        player.StartBlocking();
    }
    public void CancelBlock(){
        player.Unblocking();
    }
    public void MovingButton(string dir){
        player.MoveDirection(dir);
    }
    public void StopMoving(string dir){
        player.StopDirection(dir);
    }
    public void UpdateSkill(){
        PlayerSkillHolder s = player?.GetComponentInChildren<PlayerSkillHolder>();
        skillSlots[0].SetSkill(s.skills[0]);
        skillSlots[1].SetSkill(s.skills[1]);
        skillSlots[2].SetSkill(s.skills[2]);
        skillSlots[3].SetSkill(s.skills[3]);
    }
}
