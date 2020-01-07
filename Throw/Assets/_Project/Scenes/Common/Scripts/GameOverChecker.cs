using UnityEngine;
using UnityEngine.UI;

public class GameOverChecker : MonoBehaviour
{
    public static GameOverChecker Instance { get; private set; }
    
    [SerializeField] private float WaitOnLastScore;
    [SerializeField] private PausePanel MyPausePanel;
    [SerializeField] private GameOverPanel MyGameOverPanel;
    [SerializeField] private Slider CountdownMeter;
    [SerializeField] private float MeterIncrement;

    private float lastScoreTimestamp;
    private int previousFrameScore;
    private bool gameOver;

    void Awake()
    {
        lastScoreTimestamp = float.MaxValue;
        gameOver = false;
        Instance = this;
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

        if (CountdownMeter.gameObject.activeSelf) CountdownMeter.value = 1 - (Time.time - lastScoreTimestamp) / WaitOnLastScore; 
    }
    
    public void PressedSpace()
    {
        if (CountdownMeter.value + MeterIncrement <= 1)
        {
            lastScoreTimestamp += MeterIncrement;
        }
        else
        {
            lastScoreTimestamp = Time.time;
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
            CountdownMeter.gameObject.SetActive(true);
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
