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
    [SerializeField] private AudioSource RewardMusic;
    [SerializeField] private GameObject Confetti;
    [SerializeField] private float DimMusicRatio;
    [SerializeField] private float PayoutDivider;
    [SerializeField] private GameObject AdsButton;

    private int currentPayoutText;
    private float lastIncrementTimestamp;
    private bool beatenHighScore;
    private float waitBetweenIncrement;
    private int timesPlayed;
    private bool perfectScore;
    private bool playedRewardMusic;

    void Awake()
    {
        playedRewardMusic = false;
        beatenHighScore = false;
        lastIncrementTimestamp = 0;
        currentPayoutText = 0;
    }

    void Start()
    {
        SaveManager.Instance.SaveCurrentLevelTimesPlayed(SaveManager.Instance.LoadCurrentLevelTimesPlayed() + 1);
        PerfectScoreCheck();
        HighscoreCheck();
        SaveManager.Instance.SaveBalance(SaveManager.Instance.LoadBalance() + GetPayout());

        timesPlayed = SaveManager.Instance.LoadCurrentLevelTimesPlayed();

        CanvasThemer.Instance.Themeify();
        RefundAmmo();
    }

    void Update()
    {
        if (!playedRewardMusic && (beatenHighScore || perfectScore))
        {
            playedRewardMusic = true;
            Confetti.SetActive(true);
            MusicHandler.Instance.LevelComplete();
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

        currentPayoutText = GetPayout();
        PayoutText.text = currentPayoutText.ToString();
        PayoutModifierText.text = text;
        ScoreUpAudio.Play();
        AdsButton.SetActive(true);
    }
    
    public void ScoreUpOne()
    {
        PayoutText.text = currentPayoutText.ToString();
        if (currentPayoutText == GetRawPayout())
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

    public void WatchedAd()
    {
        SaveManager saveManager = SaveManager.Instance;
        int balance = saveManager.LoadBalance();

        saveManager.SaveBalance(balance + GetPayout());
        currentPayoutText *= 2;
        PayoutText.text = currentPayoutText.ToString();
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
        return Mathf.CeilToInt(Score.Instance.score / PayoutDivider);
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

    private void RefundAmmo()
    {
        SaveManager saveManager = SaveManager.Instance;
        BallPooler ballPooler = BallPooler.Instance;

        int normal = ballPooler.NormalBallList.Count;
        int explosive = ballPooler.ExplosiveBallList.Count;
        int guided = ballPooler.GuidedBallList.Count;
        int large = ballPooler.LargeBallList.Count;

        saveManager.SaveNormalBallCount(normal + saveManager.LoadNormalBallCount());
        saveManager.SaveExplosiveBallCount(explosive + saveManager.LoadExplosiveBallCount());
        saveManager.SaveGuidedBallCount(guided + saveManager.LoadGuidedBallCount());
        saveManager.SaveLargeBallCount(large + saveManager.LoadLargeBallCount());

        if (ChooseAmmoPanel.Instance.PicksLeft == Ammo.Instance.MaxAmmo)
        {
            saveManager.SaveCurrentLevelTimesPlayed(saveManager.LoadCurrentLevelTimesPlayed() - 1);
        }
    }
}
