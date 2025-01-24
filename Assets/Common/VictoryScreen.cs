using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text incorrectText;
    private float _startTime;

    public void Start()
    {
        _startTime = Time.time;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/TitleScreen");
    }

    public void UpdateStats()
    {
        incorrectText.text = $"Incorrect Guesses: {ThoughtBubble.IncorrectGuesses / 2}"; // I have no idea why but the incorrect guesses increments by two?
        int timeTaken = (int)(Time.time - _startTime);
        timeText.text = $"Time taken: {timeTaken}";
        int score = 2000 - ThoughtBubble.IncorrectGuesses * 50 - timeTaken;
        scoreText.text = $"Score: {score}";
    }
}
