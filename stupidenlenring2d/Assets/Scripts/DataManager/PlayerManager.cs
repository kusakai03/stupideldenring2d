using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set;}
    public event EventHandler onChangeEquipment;
    public event EventHandler onSpawnSuccess;
    public string playerName { get; private set; }
    [SerializeField] private GameObject playerObject;
    public GameObject currentPlayer;
    public string saveKey; 
    // PlayerDataSave
    public string sceneName = "MoonGraveyard";
    [SerializeField] private Vector2 savePos;
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
    [SerializeField] private int str;
    [SerializeField] private int dex;
    [SerializeField] private int intg;
    [SerializeField] private int vit;
    public List<EquipmentData> ownedEquipments;
    public List<Equipment> equipmentList;
    public List<string> gainedChestIDs;
    [Header ("Other")]
    [SerializeField] private GameObject levelupMessage;
    [SerializeField] private UISavegame campFireUI;
    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        campFireUI = FindObjectOfType<UISavegame>();
    }
    private void Update(){
        if (SceneManager.GetActiveScene().name == "MainMenu") Destroy(gameObject);
    }
    public void Campfire(){
        campFireUI.gameObject.SetActive(true);
        campFireUI.StartAnimate();
    }
    public void Respawn()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(LoadSceneAndRespawn());
        }
    }
    private IEnumerator LoadSceneAndRespawn()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        GameObject p = Instantiate(playerObject, savePos, Quaternion.identity);
        currentPlayer = p;
        SaveData();
        onSpawnSuccess?.Invoke(this, EventArgs.Empty);
    }

    public void ShowLevelupMessage(){
        Instantiate(levelupMessage, currentPlayer.transform.position, Quaternion.identity);
    }
    public Equipment GetEquipmentByID(string id){
        return equipmentList.Find(e => e.eid == id);
    }
    public void GetEquipment(string eid, int level){
        string eType = GetEquipmentByID(eid).etype;
        ownedEquipments.Add(new EquipmentData(eid, eType, level));
    }
    public void ChangeEquipment(){
        onChangeEquipment?.Invoke(this, EventArgs.Empty);
    }
    public void SetPlayerName(string playerName){
        this.playerName = playerName;
    }
    public void SaveData(){
        PlayerAttribute p = currentPlayer.GetComponent<PlayerAttribute>();
        SaveLoad.Instance.SaveData(saveKey, new PlayerDataSave(
            playerName,
            SceneManager.GetActiveScene().name,
            p.transform.position.x,
            p.transform.position.y,
            p.helmet,
            p.armor,
            p.glove,
            p.leg,
            p.weapon1,
            p.weapon2,
            p.ring1,
            p.ring2,
            p.ring3,
            p.ring4,
            p.lv.STR,
            p.lv.DEX,
            p.lv.INT,
            p.lv.VIT,
            ownedEquipments,
            gainedChestIDs,
            ItemManager.ins.itemOwneds
        ));
        sceneName = SceneManager.GetActiveScene().name;
        savePos = p.transform.position;
        helmet = p.helmet;
        armor = p.armor;
        glove = p.glove;
        leg = p.leg;
        weapon1 = p.weapon1;
        weapon2 = p.weapon2;
        ring1 = p.ring1;
        ring2 = p.ring2;
        ring3 = p.ring3;
        ring4 = p.ring4;
        str = p.lv.STR;
        dex = p.lv.DEX;
        intg = p.lv.INT;
        vit = p.lv.VIT;
    }
    public async Task LoadData()
    {
        SaveLoad saveLoad = SaveLoad.Instance;
        PlayerDataSave playerData = await saveLoad.LoadData(saveKey);
        if (playerData != null)
        {
            playerName = playerData.playerName;
            sceneName = playerData.sceneName;
            savePos = new Vector2(playerData.savePosX, playerData.savePosY);
            helmet = playerData.helmet;
            armor = playerData.armor;
            glove = playerData.glove;
            leg = playerData.leg;
            weapon1 = playerData.weapon1;
            weapon2 = playerData.weapon2;
            str = playerData.str;
            dex = playerData.dex;
            intg = playerData.intg;
            vit = playerData.vit;
            ownedEquipments = playerData.ownedEquipments;
            gainedChestIDs = playerData.gainedChestIDs;
            ItemManager.ins.itemOwneds = playerData.itemOwneds;
        }

    }
}
[Serializable] 
public class EquipmentData{
    public string eid;
    public string eType;
    [Range(1, 15)]
    public int level;
    public EquipmentData (string eid, string eType, int level){
        this.eid = eid;
        this.eType = eType;
        this.level = level;
    }
}