using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PayoutText;
    [SerializeField] private float ScoreUpTime;
         
    private int currentPayoutText;
    private int payout;
    private float lastIncrementTimestamp;
    private bool beatenHighScore;
    private float waitBetweenIncrement;

    void Awake()
    {
        beatenHighScore = false;
        lastIncrementTimestamp = 0;
        currentPayoutText = 0;
        waitBetweenIncrement = ScoreUpTime / Score.Instance.score;

        SaveManager.Instance.SaveCurrentLevelTimesPlayed(SaveManager.Instance.LoadCurrentLevelTimesPlayed() + 1);
        HighscoreCheck();
        payout = GetPayout();
        SaveManager.Instance.SaveBalance(SaveManager.Instance.LoadBalance() + payout);
    }

    void Update()
    {
        RefreshPayoutText();
    }

    public void Unpause()
    {
        Static.Unpause();
    }

    public void LoadScene(int index)
    {
        Static.LoadScene(index);
    }

    // Helpers
    private void RefreshPayoutText()
    {
        PayoutText.text = currentPayoutText.ToString();
        if (currentPayoutText != payout && Time.time - lastIncrementTimestamp > waitBetweenIncrement)
        {
            lastIncrementTimestamp = Time.time;
            currentPayoutText++;            
        }
    }

    private int GetPayout()
    {
        float multiplier = (float)1 / SaveManager.Instance.LoadCurrentLevelTimesPlayed();

        if (beatenHighScore)
        {
            multiplier *= 2;
        }

        return Mathf.CeilToInt(multiplier * Score.Instance.score);
    }

    private void HighscoreCheck()
    {
        int lastHighscore = SaveManager.Instance.LoadCurrentLevelScore();
        int score = Score.Instance.score;

        SaveManager.Instance.SaveCurrentLevelMaxScore(Score.Instance.maxScore);
        if (score > lastHighscore)
        {
            beatenHighScore = true;
            SaveManager.Instance.SaveCurrentLevelScore(score);
        }
        else
        {
            beatenHighScore = false;
        }
    }
}
