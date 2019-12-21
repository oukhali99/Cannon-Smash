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

    public static void StreamlineButton(Button streamliner, Button streamlinee)
    {

    }
    public static void StreamlineImage(Image streamliner, Image streamlinee)
    {

    }
}
