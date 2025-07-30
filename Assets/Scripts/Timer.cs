using TMPro;
using UnityEngine;

/// <summary>
/// A countdown timer (with optional count-up logic) that updates a TextMeshProUGUI text in MM:SS format.
/// When the timer hits zero, it stops and changes the text color to red.
/// </summary>
public class Timer : MonoBehaviour
{
    [Header("Timer UI")]
    [Tooltip("Text component to display the time")]
    [SerializeField] TextMeshProUGUI timerText;

    // Optional count-up logic (currently commented out)
    // float elapsedTime;

    [Header("Countdown Settings")]
    [Tooltip("Initial time in seconds to count down from")]
    [SerializeField] float remainingTime;

    // Update is called once per frame
    void Update()
    {
        // Timer (Count-Up)
        // elapsedTime += Time.deltaTime;
        // int minutes = Mathf.FloorToInt(elapsedTime / 60);
        // int seconds = Mathf.FloorToInt(elapsedTime % 60);
        // timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Countdown
        if (remainingTime > 0)
        {
            // Reduce the remaining time
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            // Clamp the timer and change text color to red
            remainingTime = 0;
            // GameOver(); // Optional game over logic
            timerText.color = Color.red;
        }

        // Format remaining time into minutes and seconds
        int minutesR = Mathf.FloorToInt(remainingTime / 60);
        int secondsR = Mathf.FloorToInt(remainingTime % 60);

        // Display the formatted time
        timerText.text = string.Format("{0:00}:{1:00}", minutesR, secondsR);
    }
}
