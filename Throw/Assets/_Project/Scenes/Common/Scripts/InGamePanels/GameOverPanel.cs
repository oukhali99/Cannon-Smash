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
    [SerializeField] private AudioSource RewardSound;
    [SerializeField] private GameObject Confetti;
    [SerializeField] private float DimMusicRatio;

    private int currentPayoutText;
    private int payout;
    private float lastIncrementTimestamp;
    private bool beatenHighScore;
    private float waitBetweenIncrement;
    private int timesPlayed;
    private bool perfectScore;
    private bool playedRewardSound;
    private float playedRewardSoundTimestamp;

    void Start()
    {
        playedRewardSound = false;
        beatenHighScore = false;
        lastIncrementTimestamp = 0;
        currentPayoutText = 0;
        playedRewardSoundTimestamp = 0;

        SaveManager.Instance.SaveCurrentLevelTimesPlayed(SaveManager.Instance.LoadCurrentLevelTimesPlayed() + 1);
        PerfectScoreCheck();
        HighscoreCheck();
        payout = GetRawPayout();

        timesPlayed = SaveManager.Instance.LoadCurrentLevelTimesPlayed();

        CanvasThemer.Instance.Themeify();
    }

    void Update()
    {
        if (!playedRewardSound && (beatenHighScore || perfectScore))
        {
            playedRewardSound = true;
            playedRewardSoundTimestamp = Time.time;

            Confetti.SetActive(true);
            MusicHandler.Instance.MusicSource.volume /= DimMusicRatio;
        }
        else if (playedRewardSound && Time.time - playedRewardSoundTimestamp > RewardSound.clip.length)
        {
            MusicHandler.Instance.MusicSource.volume *= DimMusicRatio;
        }
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
        SaveManager.Instance.SaveBalance(SaveManager.Instance.LoadBalance() + GetPayout());
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
        return Score.Instance.score;
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
