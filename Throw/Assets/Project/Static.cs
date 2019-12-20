using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Static
{
    public static float Round(float n, int dec)
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

    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public static void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void Log(bool verbose, string str)
    {
        if (verbose)
        {
            Debug.Log(str);
        }
    }

    public static void ScaleUI(Camera cam, CanvasScaler cs, float heightFactor, string gameOptionsPath)
    {
        float resHeight = cam.pixelHeight;
        cs.scaleFactor = resHeight * Options.LoadGameOptions(gameOptionsPath).UIScale / heightFactor;
    }
}
