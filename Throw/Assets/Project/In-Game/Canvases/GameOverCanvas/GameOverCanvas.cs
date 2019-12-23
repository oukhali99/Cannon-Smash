using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] private float GameOverWaitOnZeroAmmo;
    [SerializeField] private string LevelName;

    private float lastScoreTimestamp;

    // Start is called before the first frame update
    void Start()
    {
        lastScoreTimestamp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameOverCheck();
    }

    // Helpers
    private void GameOverCheck()
    {
        if (Ammo.Instance.GetAmmo() == 0 && Time.time - lastScoreTimestamp > GameOverWaitOnZeroAmmo)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        int previousMaxScore = PlayerPrefs.GetInt(LevelName + "Score");

        if (Score.Instance.score > previousMaxScore)
        {
            PlayerPrefs.SetInt(LevelName + "Score", Score.Instance.score);
            PlayerPrefs.SetInt(LevelName + "MaxScore", Score.Instance.maxScore);
        }
    }
}
