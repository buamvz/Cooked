using UnityEngine;
using UnityEngine.UI;

public class RewardSystem : MonoBehaviour
{
    [SerializeField] private CanvasGroup starContainer;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite filledStar;
    [SerializeField] private Sprite emptyStar;

    [SerializeField] private float threeStarTime = 5f;
    [SerializeField] private float twoStarTime = 10f;
    [SerializeField] private float oneStarTime = 15f;

    private void Awake()
    {
        if (starContainer != null)
        {
            starContainer.alpha = 0f;
        }
    }

    public int EvaluateStars(float elapsedSeconds)
    {
        int starCount;

        if (elapsedSeconds <= threeStarTime)
            starCount = 3;
        else if (elapsedSeconds <= twoStarTime)
            starCount = 2;
        else if (elapsedSeconds <= oneStarTime)
            starCount = 1;
        else
            starCount = 0;

        DisplayStars(starCount);

        if (starContainer != null)
        {
            starContainer.alpha = 1f;
        }

        // Try to find the current level number so we only add the difference if the player improved their score
        int levelNumber = 0;
        var levelManager = FindObjectOfType<LevelStepManager>();
        if (levelManager != null && levelManager.currentLevel != null)
        {
            levelNumber = levelManager.currentLevel.levelNumber;
        }

        if (PlayerDataManager.Instance != null && PlayerDataManager.Instance.playerProfile != null)
        {
            var added = PlayerDataManager.Instance.playerProfile.UpdateBestStarsForLevel(levelNumber, starCount);
            if (added > 0)
            {
                Debug.Log($"Added {added} star(s) to total score for level {levelNumber} (new best: {starCount})");
                // Save updated profile asynchronously (fire-and-forget)
                _ = PlayerDataManager.Instance.SavePlayerData();
            }
            else
            {
                Debug.Log($"No improvement for level {levelNumber} (best remains {PlayerDataManager.Instance.playerProfile.GetBestStarsForLevel(levelNumber)})");
            }
        }

        return starCount;
    }

    private void DisplayStars(int starCount)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = (i < starCount) ? filledStar : emptyStar;
        }
    }
}