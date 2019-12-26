using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [SerializeField] private string LevelName;
    [SerializeField] private string UIScaleSaveName;
    [SerializeField] private string PlayerNameSaveName;
    [SerializeField] private string ScoreSaveName;
    [SerializeField] private string MaxScoreSaveName;    

    void Awake()
    {
        Instance = this;
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

    // Getters
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

    // Helpers
}
