using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour
{
    public static Ammo Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private int InitialAmmo;
    [SerializeField] private Pooler BallPooler;

    public int ammo { get; private set; }

    void Awake()
    {
        ammo = InitialAmmo;
        Instance = this;
        BallPooler.PoolSize = InitialAmmo;
    }

    void Start()
    {
        RefreshText();
    }

    // Setters
    public void PlayerFires()
    {
        ammo--;
        RefreshText();
    }

    // Getters
    public int GetAmmo()
    {
        return ammo;
    }

    // Helpers
    private void RefreshText()
    {
        Text.text = ammo.ToString();
    }
}
