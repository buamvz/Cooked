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

        PlayerDataManager.Instance.playerProfile.addScore(starCount);
        
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