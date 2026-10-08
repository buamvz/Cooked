using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private float fillAmountPerClick = 0.1f;
    [SerializeField] private float fillSpeed = 2f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip moveSound;

    [SerializeField] private RewardSystem rewardSystem;

    private float targetValue = 0f;
    private bool isMoving = false;
    private bool hasStartedTiming = false;
    private bool hasCompleted = false;
    private float startTime;

    private void Start()
    {
        progressBar.value = 0f;
        targetValue = 0f;
    }

    private void Update()
    {
        if (progressBar.value != targetValue)
        {
            if (!isMoving)
            {
                isMoving = true;
                if (audioSource != null && moveSound != null)
                {
                    audioSource.clip = moveSound;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }

            progressBar.value = Mathf.MoveTowards(progressBar.value, targetValue, fillSpeed * Time.deltaTime);
        }
        else if (isMoving)
        {
            isMoving = false;
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }

        if (progressBar.value >= 1f && !hasCompleted && hasStartedTiming)
        {
            hasCompleted = true;
            float elapsed = Time.time - startTime;

            if (rewardSystem != null)
            {
                rewardSystem.EvaluateStars(elapsed);
            }
        }
    }

    public void OnButtonPressed()
    {
        if (!hasStartedTiming)
        {
            hasStartedTiming = true;
            startTime = Time.time;
        }

        targetValue = Mathf.Clamp01(targetValue + fillAmountPerClick);
    }
}