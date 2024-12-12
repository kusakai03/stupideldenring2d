using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using TMPro;
using System.Net.Http;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad Instance { get; private set;}
    public event EventHandler onAuthenticated;
    public event EventHandler onSaveSucceed;
    public event EventHandler onRequestTimeout;
    [SerializeField] private GameObject connectingMessage;
    public string saveKey;
    [SerializeField] private string auMes;
    public bool signedin;
    public bool newGame;
    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);
    }
    public async void Initialize(){
        try {
            if (!signedin){
            connectingMessage.SetActive(true);
            connectingMessage.GetComponentInChildren<TextMeshProUGUI>().text = auMes;
            await UnityServices.InitializeAsync();
            SetupEvents();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();     
            } 
        }
        catch (NotImplementedException ex) {
            Debug.LogError("Method not implemented: " + ex.Message);
            throw;
        } catch (Exception ex) {
            GameSetting.Instance.ShowConfirmMessage(ex.Message +"\nBạn không có kết nối mạng. Muốn thử lại không?", Initialize);
            onRequestTimeout?.Invoke(this, EventArgs.Empty);
            throw;
        }
    }
    private void SetupEvents() {
        AuthenticationService.Instance.SignedIn += () => {
            connectingMessage.SetActive(false);
            signedin = true;
            onAuthenticated?.Invoke(this, EventArgs.Empty);
        };
        AuthenticationService.Instance.SignInFailed += (err) => {
        };

        AuthenticationService.Instance.SignedOut += () => {
        };

        AuthenticationService.Instance.Expired += () =>
        {

        };
    }
    public async void SaveData(string key, PlayerDataSave value){
        try{
            var data = new Dictionary<string, object> {{key, value}};
            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
            onSaveSucceed?.Invoke(this, EventArgs.Empty);
        }
        catch(HttpRequestException){
            onRequestTimeout.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex) {
            Debug.LogException(ex);
        }
    }
    public async Task<PlayerDataSave> LoadData(string key) {
        try {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { key });
            if (data.ContainsKey(key)) {
                var jsonData = data[key].Value.GetAsString();
                PlayerDataSave playerData = JsonConvert.DeserializeObject<PlayerDataSave>(jsonData);
                return playerData;
            }
            return null;
        } catch (NotImplementedException ex) {
            Debug.LogError("Method not implemented: " + ex.Message);
            throw;
        } catch (Exception ex) {
            Debug.LogError("An error occurred: " + ex.Message);
            throw;
        }
    }
    public async Task<bool> KeyExists(string key)
    {
        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { key });
            return data.ContainsKey(key);
        } catch (NotImplementedException ex) {
            Debug.LogError("Method not implemented: " + ex.Message);
            throw;
        } catch (Exception ex) {
            GameSetting.Instance.ShowConfirmMessage(ex.Message +"\nBạn không có kết nối mạng. Muốn thử lại không?", Initialize);
            onRequestTimeout?.Invoke(this, EventArgs.Empty);
            throw;
        }
    }
}

[Serializable]
public class PlayerDataSave
{
    public string playerName;
    public string sceneName;
    public float savePosX;
    public float savePosY;
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
    public int str;
    public int dex;
    public int intg;
    public int vit;
    public List<EquipmentData> ownedEquipments;
    public List<string> gainedChestIDs;
    public List<ItemOwned> itemOwneds; // Thêm danh sách itemOwneds

    public PlayerDataSave(string playerName, string sceneName, float savePosX, float savePosY, EquipmentData helmet, EquipmentData armor,
                          EquipmentData glove, EquipmentData leg, EquipmentData weapon1, 
                          EquipmentData weapon2, EquipmentData ring1, EquipmentData ring2, EquipmentData ring3, EquipmentData ring4, 
                          int str, int dex, int intg, int vit, 
                          List<EquipmentData> ownedEquipments, List<string> gainedChestIDs, 
                          List<ItemOwned> itemOwneds)
    {
        this.playerName = playerName;
        this.sceneName = sceneName;
        this.savePosX = savePosX;
        this.savePosY = savePosY;
        this.helmet = helmet;
        this.armor = armor;
        this.glove = glove;
        this.leg = leg;
        this.weapon1 = weapon1;
        this.weapon2 = weapon2;
        this.ring1 = ring1;
        this.ring2 = ring2;
        this.ring3 = ring3;
        this.ring4 = ring4;
        this.str = str;
        this.dex = dex;
        this.intg = intg;
        this.vit = vit;
        this.ownedEquipments = ownedEquipments;
        this.gainedChestIDs = gainedChestIDs;
        this.itemOwneds = itemOwneds; // Gán itemOwneds từ ItemManager
    }
}
