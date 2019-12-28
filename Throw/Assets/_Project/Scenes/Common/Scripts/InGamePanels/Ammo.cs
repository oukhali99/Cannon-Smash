using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour
{
    public static Ammo Instance { get; private set; }

    [SerializeField] public int MaxAmmo;
    [SerializeField] private TextMeshProUGUI Text;

    public int ammo { get; set; }

    void Awake()
    {
        ammo = 0;
        Instance = this;
    }

    void Start()
    {
        RefreshText();
    }
    
    public void PlayerFires()
    {
        ammo--;
        RefreshText();
    }
    
    public int GetAmmo()
    {
        return ammo;
    }
    
    public void RefreshText()
    {
        Text.text = ammo.ToString();
    }
}
