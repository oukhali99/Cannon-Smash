using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BalanceText;
    [SerializeField] private int AddFundsAmount;
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

    void Start()
    {
        RefreshBalanceText();
        RefreshAmmoCounts();
    }
       
    public void ClickedAddFunds()
    {
        AddFunds(AddFundsAmount);
        RefreshBalanceText();
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

    // Helpers
    private void AddFunds(int amount)
    {
        int oldFunds = SaveManager.Instance.LoadBalance();

        SaveManager.Instance.SaveBalance(oldFunds + amount);
    }

    private void RefreshBalanceText()
    {
        BalanceText.text = SaveManager.Instance.LoadBalance().ToString() + " $";
    }

    private void RefreshAmmoCounts()
    {
        NormalAmmoCountText.text = SaveManager.Instance.LoadNormalBallCount().ToString();
        ExplosiveAmmoCountText.text = SaveManager.Instance.LoadExplosiveBallCount().ToString();
        GuidedAmmoCountText.text = SaveManager.Instance.LoadGuidedBallCount().ToString();
        LargeAmmoCountText.text = SaveManager.Instance.LoadLargeBallCount().ToString();
    }
}
