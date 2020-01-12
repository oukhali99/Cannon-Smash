using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseAmmoPanel : MonoBehaviour
{
    public static ChooseAmmoPanel Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private NormalBall NormalBall;
    [SerializeField] private ExplosiveBall ExplosiveBall;
    [SerializeField] private GuidedBall GuidedBall;
    [SerializeField] private LargeBall LargeBall;
    [SerializeField] private TextMeshProUGUI PicksLeftText;
    [SerializeField] private TextMeshProUGUI NormalBallsLeftText;
    [SerializeField] private TextMeshProUGUI ExplosiveBallsLeftText;
    [SerializeField] private TextMeshProUGUI GuidedBallsLeftText;
    [SerializeField] private TextMeshProUGUI LargeBallsLeftText;

    [HideInInspector] public int PicksLeft;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        string levelNameSaveName = SaveManager.Instance.LevelName;
        int levelNumber = int.Parse(levelNameSaveName.Substring(5, 1));
        TitleText.text = "Level " + levelNumber;

        Time.timeScale = 0;
        PicksLeft = Ammo.Instance.MaxAmmo;
        RefreshPicksLeft();
        RefreshStockText();
    }

    public void ClickedNormal()
    {
        int normalBallsLeft = SaveManager.Instance.LoadNormalBallCount();

        if (PicksLeft > 0 && normalBallsLeft > 0)
        {
            BallPooler.Instance.AddNormalBall(NormalBall);

            PicksLeft--;
            RefreshPicksLeft();

            SaveManager.Instance.SaveNormalBallCount(normalBallsLeft - 1);
            RefreshStockText();
            Ammo.Instance.ammo++;
        }
    }
    public void ClickedExplosive()
    {
        int ExplosiveBallsLeft = SaveManager.Instance.LoadExplosiveBallCount();

        if (PicksLeft > 0 && ExplosiveBallsLeft > 0)
        {
            BallPooler.Instance.AddExplosiveBall(ExplosiveBall);

            PicksLeft--;
            RefreshPicksLeft();

            SaveManager.Instance.SaveExplosiveBallCount(ExplosiveBallsLeft - 1);
            RefreshStockText();
            Ammo.Instance.ammo++;
        }
    }
    public void ClickedGuided()
    {
        int GuidedBallsLeft = SaveManager.Instance.LoadGuidedBallCount();

        if (PicksLeft > 0 && GuidedBallsLeft > 0)
        {
            BallPooler.Instance.AddAntigravtityBall(GuidedBall);

            PicksLeft--;
            RefreshPicksLeft();

            SaveManager.Instance.SaveGuidedBallCount(GuidedBallsLeft - 1);
            RefreshStockText();
            Ammo.Instance.ammo++;
        }
    }
    public void ClickedLarge()
    {
        int LargeBallsLeft = SaveManager.Instance.LoadLargeBallCount();

        if (PicksLeft > 0 && LargeBallsLeft > 0)
        {
            BallPooler.Instance.AddLargeBall(LargeBall);

            PicksLeft--;
            RefreshPicksLeft();

            SaveManager.Instance.SaveLargeBallCount(LargeBallsLeft - 1);
            RefreshStockText();
            Ammo.Instance.ammo++;
        }
    }

    public void ClickedPlay()
    {
        gameObject.SetActive(false);
        Ammo.Instance.RefreshText();
        Time.timeScale = 1;
    }

    // Helpers
    private void RefreshPicksLeft()
    {
        PicksLeftText.text = "Picks Left : " + PicksLeft.ToString();
    }

    private void RefreshStockText()
    {
        NormalBallsLeftText.text = "Left: " + SaveManager.Instance.LoadNormalBallCount();
        ExplosiveBallsLeftText.text = "Left: " + SaveManager.Instance.LoadExplosiveBallCount();
        GuidedBallsLeftText.text = "Left: " + SaveManager.Instance.LoadGuidedBallCount();
        LargeBallsLeftText.text = "Left: " + SaveManager.Instance.LoadLargeBallCount();
    }
}
