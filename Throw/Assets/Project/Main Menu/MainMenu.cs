using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MainMenu : GlobalEnvironnment
{
    public TMPro.TextMeshProUGUI GreetingsText;
    public CanvasScaler MyCanvasScaler;
    public float[] UIScales;
    public TMPro.TextMeshProUGUI NameInputText;
    public string PlayerNameDefault;
    public float UIScaleDefault;
    public float HeightFactor;
    public Camera MyCamera;
    [Header("Temeify Settings")]
    public GameObject Button1;
    public GameObject Button2;

    void Start()
    {
        FirstTimeSaveCheck();
        RefreshOptions();
        Themeify();
    }
    
    public void RefreshOptions()
    {
        ScaleUI(MyCamera, MyCanvasScaler, HeightFactor);
        GreetingsText.text = "Welcome " + PlayerPrefs.GetString("PlayerName");
    }
    public void FirstTimeSaveCheck()
    {
        if (!PlayerPrefs.HasKey("UIScale"))
        {
            PlayerPrefs.SetFloat("UIScale", UIScaleDefault);
            PlayerPrefs.SetString("PlayerName", PlayerNameDefault);
            PlayerPrefs.Save();
        }
    }

    public void SaveGUISmall()
    {
        PlayerPrefs.SetFloat("UIScale", UIScales[0]);
        PlayerPrefs.Save();
    }
    public void SaveGUIMedium()
    {
        PlayerPrefs.SetFloat("UIScale", UIScales[1]);
        PlayerPrefs.Save();
    }
    public void SaveGUILarge()
    {
        PlayerPrefs.SetFloat("UIScale", UIScales[2]);
        PlayerPrefs.Save();
    }

    public void ProcessNameInput()
    {
        string inputName = NameInputText.text;
        PlayerPrefs.SetString("PlayerName", inputName);
        PlayerPrefs.Save();
    }
    
    public void Themeify()
    {
        ThemeifyButtons("Button1", Button1);
        ThemeifyButtons("Button2", Button2);
    }
}
