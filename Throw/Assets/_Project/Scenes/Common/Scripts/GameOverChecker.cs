using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField] float WaitOnLastScore;
    [SerializeField] PausePanel MyPausePanel;
    [SerializeField] GameOverPanel MyGameOverPanel;
    
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
        if (Ammo.Instance.ammo == 0 && Time.timeScale != 0)
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
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        gameOver = true;

        MyPausePanel.gameObject.SetActive(false);
        TopRightPanel.Instance.gameObject.SetActive(false);
        MyGameOverPanel.gameObject.SetActive(true);
        CanvasThemer.Instance.Themeify();
    }
}
