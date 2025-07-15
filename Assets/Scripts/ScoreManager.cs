using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private float score;
    private float highScore;

    // Start is called before the first frame update
    void Start()
    {
        LoadScore();
        UpdateScoreText();
        UpdateHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void LoadScore()
    {
        score = 0;
        UpdateScoreText();
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        UpdateHighScoreText();
    }

    public void SaveScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    public void UpdateScore(float points)
    {
        score += points;
        UpdateScoreText();
    }
}
