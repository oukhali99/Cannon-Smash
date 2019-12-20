using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    
    public TMPro.TextMeshProUGUI NameDisplay;
    public CanvasScaler MyCanvasScaler;
    public float[] UIScales;
    public Text NameInputText;
    public string PlayerNameDefault;
    public float UIScaleDefault;
    public float HeightFactor;
    public Camera MyCamera;

    void Start()
    {
        Instance = this;

        // First time check
        if (!PlayerPrefs.HasKey("UIScale"))
        {
            PlayerPrefs.SetFloat("UIScale", UIScaleDefault);
            PlayerPrefs.SetString("PlayerName", PlayerNameDefault);
            PlayerPrefs.Save();
        }

        // Update Options
        UpdateOptions();        
    }

    public void LoadScene(int index)
    {
        Static.LoadScene(index);
    }
    public void ReloadScene()
    {
        Static.ReloadScene();
    }

    public void UpdateOptions()
    {
        Static.ScaleUI(MyCamera, MyCanvasScaler, HeightFactor);
        NameDisplay.text = "Welcome " + PlayerPrefs.GetString("PlayerName");
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
}
