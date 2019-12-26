using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] float WaitOnLastScore;
    
    private float lastScoreTimestamp;
    private int previousFrameScore;
    private bool gameOver;

    void Awake()
    {
        lastScoreTimestamp = float.MaxValue;
        gameOver = false;
    }

    void Start()
    {
        previousFrameScore = Score.Instance.score;
    }

    void Update()
    {     

        if (Ammo.Instance.ammo == 0)
        {
            ScoreCheck();
            GameOverCheck();
        }
    }

    // Helpers
    private void ScoreCheck()
    {
        int score = Score.Instance.score;

        if (score > previousFrameScore || lastScoreTimestamp == float.MaxValue)
        {
            previousFrameScore = score;
            lastScoreTimestamp = Time.time;
        }
    }

    private void GameOverCheck()
    {
        if (Time.time - lastScoreTimestamp > WaitOnLastScore && !gameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        HighScoreCheck();
        gameOver = true;

        PausePanel.Instance.gameObject.SetActive(false);
        TopRightPanel.Instance.gameObject.SetActive(false);
        GameOverPanel.Instance.gameObject.SetActive(true);
        CanvasThemer.Instance.Themeify();
    }

    private void HighScoreCheck()
    {
        int highScore = SaveManager.Instance.LoadCurrentLevelScore();

        if (Score.Instance.score > highScore)
        {
            SaveManager.Instance.SaveCurrentLevelScore(Score.Instance.score);
            SaveManager.Instance.SaveCurrentLevelMaxScore(Score.Instance.maxScore);
        }
    }
}
