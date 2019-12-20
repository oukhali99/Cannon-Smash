using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
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
        
        // Canvas Size
        Static.ScaleUI(MyCamera, MyCanvasScaler, HeightFactor);
    }

    public void UpdateScoreboard()
    {
        Instance.ScoreText.text = (Static.Round(100 * (float)Instance.Score / Instance.MaxScore, 1)).ToString() + "%";
        Instance.AmmoText.text = Instance.Ammo.ToString();
    }

    public void ClickedReset()
    {
        Static.ReloadScene();
    }
    public void ClickedMenu()
    {
        Static.LoadScene(0);
    }
}
