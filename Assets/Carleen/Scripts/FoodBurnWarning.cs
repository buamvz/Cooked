using UnityEngine;
using TMPro;

public class FoodBurnWarning : MonoBehaviour
{
    public GameObject warningIcon;

    public TMP_Text timerText;
    public TMP_Text statusText;

    public float cookingTime = 10f;
    public float warningTime = 7f;

    private float timer = 0f;
    private bool burnt = false;

    public float flashSpeed = 0.5f;
    private float flashTimer = 0f;

    void Start()
    {
        warningIcon.SetActive(false);
        statusText.text = "Cooking...";
    }

    void Update()
    {
        if (burnt)
            return;

        timer += Time.deltaTime;

        // Show timer on screen
        timerText.text = "Time: " + timer.ToString("F1") + "s";

        // Show warning
        if (timer >= warningTime)
        {
            statusText.text = "WARNING! Food is almost burnt!";

            flashTimer += Time.deltaTime;

            if (flashTimer >= flashSpeed)
            {
                warningIcon.SetActive(!warningIcon.activeSelf);
                flashTimer = 0f;
            }
        }

        // Burn food
        if (timer >= cookingTime)
        {
            BurnFood();
        }
    }

    public void ReactToWarning()
    {
        warningIcon.SetActive(false);
        statusText.text = "Cooking...";

        timer = 0f;
        flashTimer = 0f;
    }

    void BurnFood()
    {
        burnt = true;

        warningIcon.SetActive(false);
        statusText.text = "BURNT! Food is unusable.";
    }
}