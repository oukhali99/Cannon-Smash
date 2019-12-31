using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoWheel : MonoBehaviour
{
    public static AmmoWheel Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI SelectedAmmoText;
    [SerializeField] private TextMeshProUGUI SelectedAmmoCountText;
    [SerializeField] private string NormalAmmoName;
    [SerializeField] private string ExplosiveAmmoName;
    [SerializeField] private string AntigravityAmmoName;
    [SerializeField] private string LargeAmmoName;

    private int selectedIndex;
    private bool allEmpty;

    void Awake()
    {
        Instance = this;
        allEmpty = false;
        selectedIndex = 0;
    }

    void Start()
    {
        SelectedAmmoText.text = NormalAmmoName;
        BallPooler.Instance.SelectedAmmo = BallPooler.Instance.NormalBallList;
        Refresh();

        if (BallPooler.Instance.SelectedAmmo.Count == 0)
        {
            Clicked();
        }
    }

    public void Clicked()
    {
        if (allEmpty)
        {
            // Do nothing
        }
        else if (BallPooler.Instance.AllEmpty())
        {
            allEmpty = true;
        }
        else if (selectedIndex == 0)
        {
            SelectedAmmoText.text = ExplosiveAmmoName;
            BallPooler.Instance.SelectedAmmo = BallPooler.Instance.ExplosiveBallList;
            Refresh();
            selectedIndex++;
            if (BallPooler.Instance.SelectedAmmo.Count == 0)
            {
                Clicked();
            }
        }
        else if (selectedIndex == 1)
        {
            SelectedAmmoText.text = AntigravityAmmoName;
            BallPooler.Instance.SelectedAmmo = BallPooler.Instance.AntigravityBallList;
            Refresh();
            selectedIndex++;
            if (BallPooler.Instance.SelectedAmmo.Count == 0)
            {
                Clicked();
            }
        }
        else if (selectedIndex == 2)
        {
            SelectedAmmoText.text = LargeAmmoName;
            BallPooler.Instance.SelectedAmmo = BallPooler.Instance.LargeBallList;
            Refresh();
            selectedIndex++;
            if (BallPooler.Instance.SelectedAmmo.Count == 0)
            {
                Clicked();
            }
        }
        else
        {
            SelectedAmmoText.text = NormalAmmoName;
            BallPooler.Instance.SelectedAmmo = BallPooler.Instance.NormalBallList;
            Refresh();
            selectedIndex = 0;
            if (BallPooler.Instance.SelectedAmmo.Count == 0)
            {
                Clicked();
            }
        }
    }

    public void Refresh()
    {
        SelectedAmmoCountText.text = BallPooler.Instance.SelectedAmmo.Count.ToString();
    }
}
