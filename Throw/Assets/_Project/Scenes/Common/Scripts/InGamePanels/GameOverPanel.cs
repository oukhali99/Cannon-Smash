using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PayoutText;
    [SerializeField] private TextMeshProUGUI PayoutModifierText;
    [SerializeField] private Animator MyAnimator;
    [SerializeField] private AudioSource ScoreUpAudio;
         
    private int currentPayoutText;
    private int payout;
    private float lastIncrementTimestamp;
    private bool beatenHighScore;
    private float waitBetweenIncrement;
    private int timesPlayed;

    void Awake()
    {
        beatenHighScore = false;
        lastIncrementTimestamp = 0;
        currentPayoutText = 0;

        SaveManager.Instance.SaveCurrentLevelTimesPlayed(SaveManager.Instance.LoadCurrentLevelTimesPlayed() + 1);
        HighscoreCheck();
        payout = GetPayout();
        SaveManager.Instance.SaveBalance(SaveManager.Instance.LoadBalance() + payout);

        timesPlayed = SaveManager.Instance.LoadCurrentLevelTimesPlayed();
    }
    
    public void Unpause()
    {
        Static.Unpause();
    }

    public void LoadScene(int index)
    {
        Static.LoadScene(index);
    }

    public void AdjustPayoutToTimesPlayed()
    {
        string text = "/" + SaveManager.Instance.LoadCurrentLevelTimesPlayed() + " (times played today)\n";

        if (beatenHighScore)
        {
            text += "x2 (beaten daily high score)";
        }

        PayoutText.text = GetPayout().ToString();
        PayoutModifierText.text = text;
        ScoreUpAudio.Play();
    }
    
    public void ScoreUpOne()
    {
        PayoutText.text = currentPayoutText.ToString();
        if (currentPayoutText == payout * timesPlayed)
        {
            MyAnimator.SetBool("DoneScoreUp", true);
        }
        else
        {
            lastIncrementTimestamp = Time.time;
            ScoreUpAudio.Play();
            currentPayoutText++;
        }
    }

    public void DoNothing()
    {
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
