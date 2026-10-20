using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardMenu : MonoBehaviour
{
    // [SerializeField] private int playersPerPage = 25;
    [SerializeField] private LeaderboardPlayerItem playerItemPrefab;
    [SerializeField] private RectTransform playersContainer;
    // [SerializeField] public TextMeshProUGUI pageText;
    [SerializeField] private Button addScoreButton;

    [SerializeField] private SceneLoader sceneLoader;


    private async void Start()
    {
        Debug.Log($"Unity State: {UnityServices.State}");
        Debug.Log($"Signed In: {AuthenticationService.Instance.IsSignedIn}");


        Debug.Log(UnityServices.State);
        Debug.Log(AuthenticationService.Instance.IsSignedIn);

        try
        {
            var service = LeaderboardsService.Instance;
            Debug.Log("Leaderboard instance exists");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Leaderboard service failed: {ex}");
        }
        ClearPlayerList();
        addScoreButton.onClick.AddListener(AddScore);

        LoadPlayers();
    }

    private void AddScore()
    {
        LoadScoreAsync();
    }

    public async void LoadScoreAsync()
    {
        addScoreButton.interactable = false;

        try
        {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId: "Cooked_Leaderboard", PlayerDataManager.Instance.playerProfile.getTotalScore());
            LoadPlayers();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
        }
        addScoreButton.interactable = true;
    }
    private async void LoadPlayers()
    {
        try
        {
            GetScoresOptions options = new GetScoresOptions();
            options.Limit = 10;
            var scores = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId: "Cooked_Leaderboard", options);
            ClearPlayerList();
            for (int i = 0; i < scores.Results.Count; i++)
            {
                LeaderboardPlayerItem item = Instantiate(playerItemPrefab, playersContainer);
                item.Initialize(scores.Results[i]);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    private void ClearPlayerList()
    {
        LeaderboardPlayerItem[] items = playersContainer.GetComponentsInChildren<LeaderboardPlayerItem>();
        if (items != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Destroy(items[i].gameObject);
            }
        }
    }

}
