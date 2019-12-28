using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseAmmoPanel : MonoBehaviour
{
    [SerializeField] private Ball NormalBall;
    [SerializeField] private ExplosiveBall ExplosiveBall;
    [SerializeField] private AntigravityBall AntigravityBall;
    [SerializeField] private TextMeshProUGUI PicksLeftText;
    [SerializeField] private TextMeshProUGUI NormalBallsLeftText;
    [SerializeField] private TextMeshProUGUI ExplosiveBallsLeftText;
    [SerializeField] private TextMeshProUGUI AntigravityBallsLeftText;

    private int picksLeft;
    
    void Start()
    {
        Time.timeScale = 0;
        picksLeft = Ammo.Instance.MaxAmmo;
        RefreshPicksLeft();
        RefreshStockText();
    }

    public void ClickedNormal()
    {
        int normalBallsLeft = SaveManager.Instance.LoadNormalBallCount();

        if (picksLeft > 0 && normalBallsLeft > 0)
        {
            BallPooler.Instance.AddNormalBall(NormalBall);

            picksLeft--;
            RefreshPicksLeft();

            SaveManager.Instance.SaveNormalBallCount(normalBallsLeft - 1);
            RefreshStockText();
            Ammo.Instance.ammo++;
        }
    }
    public void ClickedExplosive()
    {
        int ExplosiveBallsLeft = SaveManager.Instance.LoadExplosiveBallCount();

        if (picksLeft > 0 && ExplosiveBallsLeft > 0)
        {
            BallPooler.Instance.AddExplosiveBall(ExplosiveBall);

            picksLeft--;
            RefreshPicksLeft();

            SaveManager.Instance.SaveExplosiveBallCount(ExplosiveBallsLeft - 1);
            RefreshStockText();
            Ammo.Instance.ammo++;
        }
    }
    public void ClickedAntigravity()
    {
        int AntigravityBallsLeft = SaveManager.Instance.LoadAntigravityBallCount();

        if (picksLeft > 0 && AntigravityBallsLeft > 0)
        {
            BallPooler.Instance.AddAntigravtityBall(AntigravityBall);

            picksLeft--;
            RefreshPicksLeft();

            SaveManager.Instance.SaveAntigravityBallCount(AntigravityBallsLeft - 1);
            RefreshStockText();
            Ammo.Instance.ammo++;
        }
    }

    public void ClickedPlay()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Ammo.Instance.RefreshText();

        if (Ammo.Instance.ammo == 0)
        {
            Ammo.Instance.ammo--;
        }
    }

    // Helpers
    private void RefreshPicksLeft()
    {
        PicksLeftText.text = "Picks Left : " + picksLeft.ToString();
    }

    private void RefreshStockText()
    {
        NormalBallsLeftText.text = "Left: " + SaveManager.Instance.LoadNormalBallCount();
        ExplosiveBallsLeftText.text = "Left: " + SaveManager.Instance.LoadExplosiveBallCount();
        AntigravityBallsLeftText.text = "Left: " + SaveManager.Instance.LoadAntigravityBallCount();
    }
}
