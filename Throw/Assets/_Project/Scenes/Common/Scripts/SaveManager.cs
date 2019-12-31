using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public string LevelName;
    [SerializeField] private string UIScaleSaveName;
    [SerializeField] private string PlayerNameSaveName;
    [SerializeField] private string ScoreSaveName;
    [SerializeField] private string MaxScoreSaveName;
    [SerializeField] private string LevelTimesPlayedSaveName;
    [Header("Ammo Names")]
    [SerializeField] private string NormalBallCountSaveName;
    [SerializeField] private string ExplosiveBallCountSaveName;
    [SerializeField] private string AntigravityBallCountSaveName;
    [SerializeField] private string LargeBallCountSaveName;

    void Awake()
    {
        Instance = this;

        if (!PlayerPrefs.HasKey(UIScaleSaveName))
        {
            FirstTime();
        }
    }

    private void FirstTime()
    {
        PlayerPrefs.SetInt(UIScaleSaveName, 2);
        PlayerPrefs.SetInt("Balance", 10);
    }

    public void SaveNormalBallCount(int newCount)
    {
        PlayerPrefs.SetInt(NormalBallCountSaveName, newCount);
    }
    public void SaveExplosiveBallCount(int newCount)
    {
        PlayerPrefs.SetInt(ExplosiveBallCountSaveName, newCount);
    }
    public void SaveAntigravityBallCount(int newCount)
    {
        PlayerPrefs.SetInt(AntigravityBallCountSaveName, newCount);
    }
    public void SaveLargeBallCount(int newCount)
    {
        PlayerPrefs.SetInt(LargeBallCountSaveName, newCount);
    }

    public void ClearSavedData()
    {
        PlayerPrefs.DeleteAll();
        FirstTime();
    }

    public void SaveUIScale(int scale)
    {
        PlayerPrefs.SetInt(UIScaleSaveName, scale);
        PlayerPrefs.Save();
    }

    public void SavePlayerName(string name)
    {
        PlayerPrefs.SetString(PlayerNameSaveName, name);
    }

    public void SaveScore(string levelName, int score)
    {
        PlayerPrefs.SetInt(levelName + ScoreSaveName, score);
    }

    public void SaveMaxScore(string levelName, int maxScore)
    {
        PlayerPrefs.SetInt(levelName + MaxScoreSaveName, maxScore);
    }

    public void SaveCurrentLevelScore(int score)
    {
        SaveScore(LevelName, score);
    }

    public void SaveCurrentLevelMaxScore(int maxScore)
    {
        SaveMaxScore(LevelName, maxScore);
    }

    public void SaveBalance(int newBalance)
    {
        PlayerPrefs.SetInt("Balance", newBalance);
    }

    public void SaveCurrentLevelTimesPlayed(int timesPlayed)
    {
        PlayerPrefs.SetInt(LevelName + LevelTimesPlayedSaveName, timesPlayed);
    }

    // Getters
    public int LoadNormalBallCount()
    {
        return PlayerPrefs.GetInt(NormalBallCountSaveName);
    }
    public int LoadExplosiveBallCount()
    {
        return PlayerPrefs.GetInt(ExplosiveBallCountSaveName);
    }
    public int LoadAntigravityBallCount()
    {
        return PlayerPrefs.GetInt(AntigravityBallCountSaveName);
    }
    public int LoadLargeBallCount()
    {
        return PlayerPrefs.GetInt(LargeBallCountSaveName);
    }

    public int LoadUIScale()
    {
        return PlayerPrefs.GetInt(UIScaleSaveName);
    }

    public string LoadPlayerName()
    {
        return PlayerPrefs.GetString(PlayerNameSaveName);
    }

    public int LoadScore(string levelName)
    {
        return PlayerPrefs.GetInt(levelName + ScoreSaveName);
    }

    public int LoadMaxScore(string levelName)
    {
        return PlayerPrefs.GetInt(levelName + MaxScoreSaveName);
    }

    public int LoadCurrentLevelScore()
    {
        return LoadScore(LevelName);
    }

    public int LoadBalance()
    {
        return PlayerPrefs.GetInt("Balance");
    }

    public int LoadCurrentLevelTimesPlayed()
    {
        return PlayerPrefs.GetInt(LevelName + LevelTimesPlayedSaveName);
    }
}
