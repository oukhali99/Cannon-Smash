using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [SerializeField] private string LevelName;
    [SerializeField] private string UIScaleSaveName;
    [SerializeField] private string PlayerNameSaveName;
    [SerializeField] private string ScoreSaveName;
    [SerializeField] private string MaxScoreSaveName;
    [SerializeField] private string NormalBallCountSaveName;
    [SerializeField] private string ExplosiveBallCountSaveName;
    [SerializeField] private string AntigravityBallCountSaveName;

    void Awake()
    {
        Instance = this;
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

    public void ClearSavedData()
    {
        PlayerPrefs.DeleteAll();
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

    // Helpers
}
