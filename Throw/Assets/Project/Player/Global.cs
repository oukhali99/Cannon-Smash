using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance;

    public int Score;
    public int MaxScore;
    public int Ammo;
    public Pooler BallPooler;
    public TMPro.TextMeshProUGUI ScoreText;
    public TMPro.TextMeshProUGUI AmmoText;
    
    // Use this for initialization
    void Awake ()
    {
        Instance = this;
        Application.targetFrameRate = 30;
	}

    void Start()
    {
        Instance.Ammo = BallPooler.Pool.Length;
        UpdateScoreboard();
    }

    public static void UpdateScoreboard()
    {
        Instance.ScoreText.text = (Round(100 * (float)Instance.Score / Instance.MaxScore, 1)).ToString() + "%";
        Instance.AmmoText.text = Instance.Ammo.ToString();
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
