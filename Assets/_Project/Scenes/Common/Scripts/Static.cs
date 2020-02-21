using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class contains commonly used static functions
/// </summary>
public class Static
{
    public static void LoadScene(int index)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(index);
    }
    public static void ReloadScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void LoadNextScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static string GetPercentageString(int score, int maxScore)
    {
        if (maxScore != 0)
        {
            float percentage = 100 * (float)score / maxScore;

            return Round(percentage, 1).ToString() + "%";
        }
        else
        {
            return "0%";
        }
    }

    private static float Round(float n, int dec)
    {
        for (int i = 0; i < dec; i++)
        {
            n *= 10;
        }

        n = Mathf.Floor(n);

        for (int i = 0; i < dec; i++)
        {
            n /= 10;
        }

        return n;
    }

    public static void Pause()
    {
        Time.timeScale = 0;
    }
    public static void Unpause()
    {
        Time.timeScale = 1;
    }
}
