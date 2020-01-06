using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Store : MonoBehaviour
{
    public static Store Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI BalanceText;
    [SerializeField] private int PaycheckAmmount;
    [SerializeField] private int BuyNormalAmmoCount;    
    [SerializeField] private int BuyExplosiveAmmoCount;
    [SerializeField] private int BuyGuidedAmmoCount;
    [SerializeField] private int BuyLargeAmmoCount;
    [SerializeField] private int NormalAmmoPrice;
    [SerializeField] private int ExplosiveAmmoPrice;
    [SerializeField] private int GuidedAmmoPrice;
    [SerializeField] private int LargeAmmoPrice;
    [SerializeField] private TextMeshProUGUI NormalAmmoCountText;
    [SerializeField] private TextMeshProUGUI ExplosiveAmmoCountText;
    [SerializeField] private TextMeshProUGUI GuidedAmmoCountText;
    [SerializeField] private TextMeshProUGUI LargeAmmoCountText;
    [SerializeField] private double LoginGapDay;
    [SerializeField] private Button PaycheckButton;
    [SerializeField] private Button PlayButton;
    [SerializeField] private AudioSource MoneyAudio;

    private bool isDailyLogin;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RefreshBalanceText();
        RefreshAmmoCounts();

        DateTime lastLogin = SaveManager.Instance.LoadLastLogin();
        TimeSpan loginDifference = DateTime.Now.Subtract(lastLogin);

        if (loginDifference.TotalDays > LoginGapDay)
        {
            isDailyLogin = true;
            PaycheckButton.gameObject.SetActive(true);
            PlayButton.gameObject.SetActive(false);
        }
        else
        {
            isDailyLogin = false;
            PaycheckButton.gameObject.SetActive(false);
            PlayButton.gameObject.SetActive(true);
        }
    }
       
    public void ClickedPaycheck()
    {
        MoneyAudio.Play();
        AddFunds(PaycheckAmmount);
        RefreshBalanceText();
        CollectedPaycheck();
    }
    public void ClickedBuyNormalAmmo()
    {
        if (SaveManager.Instance.LoadBalance() >= NormalAmmoPrice)
        {
            int lastCount = SaveManager.Instance.LoadNormalBallCount();
            SaveManager.Instance.SaveNormalBallCount(lastCount + BuyNormalAmmoCount);
            AddFunds(-NormalAmmoPrice);
            RefreshBalanceText();
            RefreshAmmoCounts();
        }
    }
    public void ClickedBuyExplosiveAmmo()
    {
        if (SaveManager.Instance.LoadBalance() >= ExplosiveAmmoPrice)
        {
            int lastCount = SaveManager.Instance.LoadExplosiveBallCount();
            SaveManager.Instance.SaveExplosiveBallCount(lastCount + BuyExplosiveAmmoCount);
            AddFunds(-ExplosiveAmmoPrice);
            RefreshBalanceText();
            RefreshAmmoCounts();
        }        
    }
    public void ClickedBuyGuidedAmmo()
    {
        if (SaveManager.Instance.LoadBalance() >= GuidedAmmoPrice)
        {
            int lastCount = SaveManager.Instance.LoadGuidedBallCount();
            SaveManager.Instance.SaveGuidedBallCount(lastCount + BuyGuidedAmmoCount);
            AddFunds(-GuidedAmmoPrice);
            RefreshBalanceText();
            RefreshAmmoCounts();
        }            
    }
    public void ClickedBuyLargeAmmo()
    {
        if (SaveManager.Instance.LoadBalance() >= LargeAmmoPrice)
        {
            int lastCount = SaveManager.Instance.LoadLargeBallCount();
            SaveManager.Instance.SaveLargeBallCount(lastCount + BuyLargeAmmoCount);
            AddFunds(-LargeAmmoPrice);
            RefreshBalanceText();
            RefreshAmmoCounts();
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

    public void CollectedPaycheck()
    {

        SaveManager.Instance.SaveLastLogin(DateTime.Now);
        PaycheckButton.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(true);
    }

    // Helpers
    private void AddFunds(int amount)
    {
        int oldFunds = SaveManager.Instance.LoadBalance();

        SaveManager.Instance.SaveBalance(oldFunds + amount);
    }

    private void RefreshAmmoCounts()
    {
        NormalAmmoCountText.text = SaveManager.Instance.LoadNormalBallCount().ToString();
        ExplosiveAmmoCountText.text = SaveManager.Instance.LoadExplosiveBallCount().ToString();
        GuidedAmmoCountText.text = SaveManager.Instance.LoadGuidedBallCount().ToString();
        LargeAmmoCountText.text = SaveManager.Instance.LoadLargeBallCount().ToString();
    }

    private void RefreshBalanceText()
    {
        BalanceText.text = SaveManager.Instance.LoadBalance() + "$";
    }
}
