using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame : GlobalEnvironnment
{
    public static InGame Instance;
    public int Score;
    public int MaxScore;
    public int Ammo;
    public Pooler BallPooler;
    public TMPro.TextMeshProUGUI ScoreText;
    public TMPro.TextMeshProUGUI AmmoText;
    public Camera MyCamera;
    public UnityEngine.UI.CanvasScaler MyCanvasScaler;
    public float HeightFactor;
    [Header("Temeify Settings")]
    public GameObject Button1;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 30;
	}

    void Start()
    {
        Ammo = BallPooler.Pool.Length;
        RefreshUI();
        ScaleUI(MyCamera, MyCanvasScaler, HeightFactor);
        Themeify();
    }

    public void RefreshUI()
    {
        ScoreText.text = (Static.Round(100 * (float)Score / MaxScore, 1)).ToString() + "%";
        AmmoText.text = Ammo.ToString();
    }

    public void Themeify()
    {
        ThemeifyButtons("Button1", Button1);
    }
}
