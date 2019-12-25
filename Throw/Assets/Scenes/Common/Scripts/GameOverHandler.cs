using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] float WaitOnLastScore;
    
    private float lastScoreTimestamp;
    private int previousFrameScore;

    void Awake()
    {
        lastScoreTimestamp = float.MaxValue;
    }

    void Start()
    {
        previousFrameScore = Score.Instance.score;
    }

    void Update()
    {     

        if (Ammo.Instance.ammo == 0)
        {
            int score = Score.Instance.score;

            if (score > previousFrameScore || lastScoreTimestamp == float.MaxValue)
            {
                lastScoreTimestamp = Time.time;
            }

            if (Time.time - lastScoreTimestamp > WaitOnLastScore)
            {
                GameOver();
            }
        }
    }

    // Helpers
    private void GameOver()
    {
        HighScoreCheck();

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
