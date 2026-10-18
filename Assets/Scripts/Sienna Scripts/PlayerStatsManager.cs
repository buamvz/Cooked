using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    // add level to current player profile
    public void AddCompletedLevel()
    {
        PlayerDataManager.Instance.playerProfile.addCompletedLevel();
    }

    // add stars/points to player
    public void AddScore(int score)
    {
        PlayerDataManager.Instance.playerProfile.addScore(score);
    }


}
