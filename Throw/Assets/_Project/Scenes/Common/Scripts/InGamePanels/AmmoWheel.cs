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

    private int selectedIndex;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        selectedIndex = 0;
        SelectedAmmoText.text = NormalAmmoName;
        BallPooler.Instance.SelectedAmmo = BallPooler.Instance.NormalBallList;
        Refresh();
    }

    public void Clicked()
    {
        if (selectedIndex == 0)
        {
            SelectedAmmoText.text = ExplosiveAmmoName;
            BallPooler.Instance.SelectedAmmo = BallPooler.Instance.ExplosiveBallList;
            Refresh();
            selectedIndex = 1;
        }
        else if (selectedIndex == 1)
        {
            SelectedAmmoText.text = AntigravityAmmoName;
            BallPooler.Instance.SelectedAmmo = BallPooler.Instance.AntigravityBallList;
            Refresh();
            selectedIndex = 2;
        }
        else
        {
            SelectedAmmoText.text = NormalAmmoName;
            BallPooler.Instance.SelectedAmmo = BallPooler.Instance.NormalBallList;
            Refresh();
            selectedIndex = 0;
        }
    }

    public void Refresh()
    {
        SelectedAmmoCountText.text = BallPooler.Instance.SelectedAmmo.Count.ToString();
    }
}
