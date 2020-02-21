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
    [SerializeField] private GameObject Confetti;
    [SerializeField] private float DimMusicRatio;
    [SerializeField] private float PayoutDivider;
    [SerializeField] private GameObject AdsButton;
    [SerializeField] private GameObject MainMenuButton;
    [SerializeField] private GameObject ContinueButton;

    private int currentPayoutText;
    private bool beatenHighScore;
    private int timesPlayed;
    private bool perfectScore;
    private bool celebrated;

    void Awake()
    {
        currentPayoutText = 0;
        beatenHighScore = false;
        timesPlayed = 1;
        perfectScore = false;
        celebrated = false;
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
        MusicHandler.Instance.LevelComplete();
    }

    void Update()
    {
        if (!celebrated && (beatenHighScore || perfectScore))
        {
            celebrated = true;
            Confetti.SetActive(true);
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
        MainMenuButton.SetActive(true);
        ContinueButton.SetActive(true);
        CanvasThemer.Instance.Themeify();
    }
    
    public void ScoreUpOne()
    {
        if (currentPayoutText >= GetRawPayout())
        {
            MyAnimator.SetBool("DoneScoreUp", true);
        }
        else if (!MyAnimator.GetBool("DoneScoreUp"))
        {
            ScoreUpAudio.Play();
            currentPayoutText++;
            PayoutText.text = currentPayoutText.ToString();
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
