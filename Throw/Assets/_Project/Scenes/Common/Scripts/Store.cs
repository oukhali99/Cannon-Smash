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
    [SerializeField] private int BuyAntigravityAmmoCount;
    [SerializeField] private int NormalAmmoPrice;
    [SerializeField] private int ExplosiveAmmoPrice;
    [SerializeField] private int AntigravityAmmoPrice;
    [SerializeField] private TextMeshProUGUI NormalAmmoCountText;
    [SerializeField] private TextMeshProUGUI ExplosiveAmmoCountText;
    [SerializeField] private TextMeshProUGUI AntigravityAmmoCountText;

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
    public void ClickedBuyAntigravityAmmo()
    {
        if (SaveManager.Instance.LoadBalance() >= AntigravityAmmoPrice)
        {
            int lastCount = SaveManager.Instance.LoadAntigravityBallCount();
            SaveManager.Instance.SaveAntigravityBallCount(lastCount + BuyAntigravityAmmoCount);
            AddFunds(-AntigravityAmmoPrice);
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
        AntigravityAmmoCountText.text = SaveManager.Instance.LoadAntigravityBallCount().ToString();
    }
}
