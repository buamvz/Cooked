using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudCode;
using Unity.Services.CloudCode.GeneratedBindings;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    // Reset player data but preserve player name and in-memory instance.
    // Clears completed levels, total score and per-level bests, resets unlocked level PlayerPrefs,
    // and attempts to update runtime UI and level state. Saves the cleared data.
    public async Task ResetPlayerData()
    {
        if (playerProfile == null)
        {
            Debug.LogWarning("PlayerProfile is null; creating new profile to reset data.");
            playerProfile = new PlayerProfile();
        }

        // preserve name
        var preservedName = playerProfile.playerName;

        playerProfile.playerName = preservedName;
        playerProfile.setCompletedLevels(0);
        // clear best stars and total score
        playerProfile.ClearBestStars();
        playerProfile.setTotalScore(0);

        // reset unlocked level
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();

        // update any LevelStepManager instances so levels are not marked completed
        var levelManagers = FindObjectsOfType<LevelStepManager>();
        foreach (var lm in levelManagers)
        {
            if (lm != null && lm.currentLevel != null)
            {
                lm.currentLevel.levelIsCompleted = false;
            }
        }

        // update profile UI
        var profilePages = FindObjectsOfType<ProfilePageManager>();
        foreach (var page in profilePages)
        {
            page.UpdateProfileInfo();
        }

        // update level menus (buttons) if present
        var levelMenus = FindObjectsOfType<LevelMenu>();
        var unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        foreach (var menu in levelMenus)
        {
            if (menu != null && menu.buttons != null)
            {
                for (int i = 0; i < menu.buttons.Length; i++)
                {
                    menu.buttons[i].interactable = i < unlocked;
                }
            }
        }

        // save cleared data
        await SavePlayerData();

        Debug.Log("Player data reset (player name preserved).");
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
            { "completedLevels", playerProfile.getCompletedLevels() },
            { "totalScore", playerProfile.getTotalScore() },
            { "bestStars", playerProfile.GetBestStarsForSave() }
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
                "totalScore",
                "bestStars"
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
                playerProfile.setCompletedLevels(completedLevels.Value.GetAs<int>());
                Debug.Log($"Completed Levels: {completedLevels.Value.GetAs<string>()}");
            }
            // bestStars, if present, should be used to reconstruct totalScore (keeps consistency)
            bool bestStarsLoaded = false;
            if (playerData.TryGetValue("bestStars", out var bestStars))
            {
                try
                {
                    var dict = bestStars.Value.GetAs<Dictionary<string, int>>();
                    playerProfile.SetBestStarsFromSave(dict);
                    bestStarsLoaded = true;
                    Debug.Log($"Loaded best stars for {dict.Count} levels.");
                }
                catch (System.Exception)
                {
                    Debug.LogWarning("Failed to parse bestStars from save; falling back to totalScore if available.");
                }
            }

            if (!bestStarsLoaded && playerData.TryGetValue("totalScore", out var totalScore))
            {
                playerProfile.setTotalScore(totalScore.Value.GetAs<int>());
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
