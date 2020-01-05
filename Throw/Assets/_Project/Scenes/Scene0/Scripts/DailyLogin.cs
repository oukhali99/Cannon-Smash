using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyLogin : MonoBehaviour
{
    public static DailyLogin Instance { get; private set; }

    [SerializeField] private int Paycheck;
    [SerializeField] private double LoginGapDay;

    private bool isDailyLogin;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DateTime lastLogin = SaveManager.Instance.LoadLastLogin();
        TimeSpan loginDifference = DateTime.Now.Subtract(lastLogin);

        if (loginDifference.TotalMinutes > LoginGapDay)
        {
            isDailyLogin = true;
            Debug.Log("Negroid");

            SaveManager.Instance.SaveLastLogin(DateTime.Now);
        }
        else
        {
            isDailyLogin = false;
        }
    }

    public void ResetScore(string levelName)
    {
        if (isDailyLogin)
        {
            SaveManager.Instance.SaveScore(levelName, 0);
            SaveManager.Instance.SaveMaxScore(levelName, 0);
            SaveManager.Instance.SaveLevelTimesPlayed(levelName, 0);
        }
    }
}
