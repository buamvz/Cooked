using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Leaderboards.Models;

public class LeaderboardPlayerItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button selectButton;

    private LeaderboardEntry player;

    private void Start()
    {
        selectButton.onClick.AddListener(Clicked);
    }

    public void Initialize(LeaderboardEntry player)
    {
        this.player = player;
        rankText.text = (player.Rank + 1).ToString();
        nameText.text = player.PlayerName;
        scoreText.text = player.Score.ToString();
    }
    private void Clicked()
    {
        Debug.Log(message: "TODO -> Open profile: " + player.PlayerName);
    }
}
