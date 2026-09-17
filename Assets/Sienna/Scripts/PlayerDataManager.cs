using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudCode;
using Unity.Services.CloudCode.GeneratedBindings;
using Unity.Services.CloudSave;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerDataManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // save data with unity cloud save
    public async Task SavePlayerData(string playerName, int completedLevels, int totalScore)  // may need to adjust these according to how score and level data tracks
    {
        var playerData = new Dictionary<string, object>
        {
            { "playerName", playerName },
            { "completedLevels", completedLevels },
            { "totalScore", totalScore }
        };

        try
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
            Debug.Log("Saved player data...!");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to save player data: {ex}");
        }
    }

    public async Task LoadPlayerData()
    {
        try
        {
            var keys = new HashSet<string>
            {
                "playerName",
                "completedLevels",
                "totalScore"
            };

            var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

            // debug write player data for reference
            if (playerData.TryGetValue("playerName", out var name))
            {
                Debug.Log($"Player Name: {name.Value.GetAs<string>()}");
            }
            if (playerData.TryGetValue("completedLevels", out var completedLevels))
            {
                Debug.Log($"Completed Levels: {completedLevels.Value.GetAs<string>()}");
            }
            if (playerData.TryGetValue("totalScore", out var totalScore))
            {
                Debug.Log($"Total Score: {totalScore.Value.GetAs<string>()}");
            }

        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load player data: {ex}");
        }
    }

    //public MyModuleBindings MyModuleBindings;
    //public LoginManager LoginManager;
    //public string PlayerName;

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    LoginManager.PlayerSignedIn += InitializePlayer;

    //    MyModuleBindings = new MyModuleBindings(CloudCodeService.Instance);
    //}

    //public async void InitializePlayer()
    //{
    //    try
    //    {
    //        var resultFromCloud = await MyModuleBindings.SayHello(PlayerName);
    //        Debug.Log($"{resultFromCloud}");
    //    }
    //    catch (CloudCodeException ex)
    //    {
    //        Debug.LogException(ex);
    //    }
    //}

    //private void OnDisable()
    //{
    //    LoginManager.PlayerSignedIn -= InitializePlayer;
    //}
}
