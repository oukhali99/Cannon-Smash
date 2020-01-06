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
    private bool perfectScore;

    void Start()
    {
        beatenHighScore = false;
        lastIncrementTimestamp = 0;
        currentPayoutText = 0;

        SaveManager.Instance.SaveCurrentLevelTimesPlayed(SaveManager.Instance.LoadCurrentLevelTimesPlayed() + 1);
        PerfectScoreCheck();
        HighscoreCheck();
        payout = GetRawPayout();
        SaveManager.Instance.SaveBalance(SaveManager.Instance.LoadBalance() + payout);

        timesPlayed = SaveManager.Instance.LoadCurrentLevelTimesPlayed();

        CanvasThemer.Instance.Themeify();
    }
    
    public void Unpause()
    {
        Static.Unpause();
    }

    public void LoadScene(int index)
    {
        Static.LoadScene(index);
    }

    public void LoadNextScene()
    {
        Static.LoadNextScene();
    }

    public void AdjustPayoutToTimesPlayed()
    {
        string text = "/" + SaveManager.Instance.LoadCurrentLevelTimesPlayed() + " (times played today)\n";

        if (beatenHighScore)
        {
            text += "x2 (beaten daily high score)\n";
        }
        if (perfectScore)
        {
            text += "x2 (perfect score!)";
        }

        PayoutText.text = GetPayout().ToString();
        PayoutModifierText.text = text;
        ScoreUpAudio.Play();
    }
    
    public void ScoreUpOne()
    {
        PayoutText.text = currentPayoutText.ToString();
        if (currentPayoutText == payout)
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
        if (perfectScore)
        {
            multiplier *= 2;
        }

        return Mathf.CeilToInt(multiplier * GetRawPayout());
    }

    private int GetRawPayout()
    {
        return Mathf.CeilToInt(Score.Instance.score / 4);
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

    private void PerfectScoreCheck()
    {
        if (Score.Instance.score == Score.Instance.maxScore)
        {
            perfectScore = true;
        }
        else
        {
            perfectScore = false;
        }
    }
}
