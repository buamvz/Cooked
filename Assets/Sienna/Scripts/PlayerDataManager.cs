using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudCode;
using Unity.Services.CloudCode.GeneratedBindings;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    public PlayerProfile playerProfile;

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
    // updated to use player profile class
    public async Task SavePlayerData()
    {
        if (playerProfile == null)
        {
            Debug.LogError("Player Profile is null, cannot save.");
            return;
        }
        // check that unity services is initialized/active or the data can't save
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            Debug.LogError("Unity Services not initialized, cannot save player data.");
            return;
        }
        // make sure player is signed in or data cant save - cant save info when anonymous
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.LogError("Player is not signed in, cannot save player data.");
            return;
        }

        var playerData = new Dictionary<string, object>
        {
            { "playerName", playerProfile.playerName },
            { "completedLevels", playerProfile.completedLevels },
            { "totalScore", playerProfile.totalScore }
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

            playerProfile = new PlayerProfile();

            // debug write player data for reference
            if (playerData.TryGetValue("playerName", out var name))
            {
                playerProfile.playerName = name.Value.GetAs<string>();
                Debug.Log($"Player Name: {name.Value.GetAs<string>()}");
            }
            if (playerData.TryGetValue("completedLevels", out var completedLevels))
            {
                playerProfile.completedLevels = completedLevels.Value.GetAs<int>();
                Debug.Log($"Completed Levels: {completedLevels.Value.GetAs<string>()}");
            }
            if (playerData.TryGetValue("totalScore", out var totalScore))
            {
                playerProfile.totalScore = totalScore.Value.GetAs<int>();
                Debug.Log($"Total Score: {totalScore.Value.GetAs<string>()}");
            }
            Debug.Log($"Loaded player profile: {playerProfile.playerName}");
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
