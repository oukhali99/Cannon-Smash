using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance;
    public static int Score = 0;
    public static int MaxScore = 0;
    public static int Ammo;

    public UnityEngine.UI.Text ScoreText;
    public UnityEngine.UI.Text AmmoText;

    // Use this for initialization
    void Awake ()
    {
        Instance = this;
	}

    void Start()
    {
        UpdateScoreboard();
    }

    public static void UpdateScoreboard()
    {
        Instance.ScoreText.text = (Round(100 * (float)Global.Score / Global.MaxScore, 1)).ToString() + "%";
        Instance.AmmoText.text = Ammo.ToString();
    }

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
}
