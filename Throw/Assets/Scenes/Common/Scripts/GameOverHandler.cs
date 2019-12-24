using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] float WaitOnLastScore;

    private float lastScoreTimestamp;
    private int lastFrameScore;
    private bool gameOver;

    void Awake()
    {
        lastScoreTimestamp = 0;
        lastFrameScore = Score.Instance.score;
        gameOver = false;
    }

    void Update()
    {
        ScoreCheck();
        GameOverCheck();
    }

    // Helpers
    private void ScoreCheck()
    {
        if (Ammo.Instance.ammo == 0)
        {
            int score = Score.Instance.score;

            if (score > lastFrameScore)
            {
                lastFrameScore = score;
                lastScoreTimestamp = Time.time;
            }
        }
    }

    private void GameOverCheck()
    {
        int ammoLeft = Ammo.Instance.ammo;

        if (!gameOver && ammoLeft == 0 && Time.time - lastScoreTimestamp > WaitOnLastScore && lastScoreTimestamp != 0)
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
        Time.timeScale = 0;
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
