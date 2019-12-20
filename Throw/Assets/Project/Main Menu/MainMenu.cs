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

    public bool Verbose;
    public TMPro.TextMeshProUGUI NameDisplay;
    public CanvasScaler MyCanvasScaler;
    public float[] UIScales;
    public Text NameInputText;
    public string PlayerNameDefault;
    public float UISizeDefault;
    public string GameOptionsFileName;
    public float HeightFactor;
    public Camera MyCamera;

    void Start()
    {
        Instance = this;

        // First time check
        Options.GameOptionsCheck(GameOptionsFileName, new Options.GameOptions(UISizeDefault, PlayerNameDefault));

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
        Static.ScaleUI(MyCamera, MyCanvasScaler, HeightFactor, GameOptionsFileName);
        NameDisplay.text = "Welcome " + Options.LoadGameOptions(GameOptionsFileName).PlayerName;
    }

    public void SaveGUISmall()
    {
        Options.SaveGUI(0, GameOptionsFileName);
    }
    public void SaveGUIMedium()
    {
        Options.SaveGUI(1, GameOptionsFileName);
    }
    public void SaveGUILarge()
    {
        Options.SaveGUI(2, GameOptionsFileName);
    }

    public void ProcessNameInput()
    {
        string inputName = NameInputText.text;
        Options.GameOptions gameOptions = Options.LoadGameOptions(GameOptionsFileName);
        gameOptions.PlayerName = inputName;
        Options.SaveGameOptions(gameOptions, GameOptionsFileName);
    }
}
