using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    // The amount of time the player has to complete the level.
    // 180 seconds = 3 minutes.
    // [SerializeField] allows us to change this value in the Unity Inspector.
    [SerializeField] private float parTime = 180f;

    // The TextMeshPro UI text that will display the timer on the screen.
    // We connect this to our TimerText object in the Unity Inspector.
    [SerializeField] private TMP_Text timerText;

    // Stores the player's current remaining time.
    private float currentTime;

    // Keeps track of whether the player has gone over the par time.
    private bool isOvertime = false;


    // Start() runs once when the level begins.
    void Start()
    {
        // Set the current time to the level's par time.
        // For example, 180 seconds will start the timer at 03:00.
        currentTime = parTime;

        // Display the starting time on the screen.
        UpdateTimerDisplay();
    }


    // Update() runs once every frame while the game is running.
    void Update()
    {
        // Only decrease the timer while there is still time remaining.
        // This also prevents the timer from going into negative numbers.
        if (currentTime > 0)
        {
            // Subtract the amount of time that has passed since the
            // previous frame.
            currentTime -= Time.deltaTime;

            // Check if the timer has reached zero.
            if (currentTime <= 0)
            {
                // Make sure the timer stops exactly at 00:00
                // instead of showing a negative number.
                currentTime = 0;

                // The player has now gone into overtime.
                isOvertime = true;
            }

            // Update the timer displayed on the screen.
            UpdateTimerDisplay();
        }
    }


    // Converts the remaining time from seconds into minutes and seconds
    // and displays it on the UI.
    private void UpdateTimerDisplay()
    {
        // Calculate how many whole minutes are remaining.
        // Example: 125 seconds = 2 minutes.
        int minutes = Mathf.FloorToInt(currentTime / 60);

        // Calculate the remaining seconds after the minutes are removed.
        // Example: 125 seconds = 5 remaining seconds.
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Display the timer in MM:SS format.
        // Example: 2 minutes and 5 seconds becomes 02:05.
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}