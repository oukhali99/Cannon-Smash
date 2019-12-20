using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text NameInputField;
    public TMPro.TextMeshProUGUI NameDisplay;
    public CanvasScaler MyCanvasScaler;
    public Slider GUIScaleSlider;

    void Start()
    {
        RefreshDisplayedName();
        RefreshGUIScale();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ReloadScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        LoadScene(index);
    }

    // Welcome Name
    public void RefreshDisplayedName()
    {
        NameDisplay.text = "Welcome " + LoadProperty("playerName");
    }

    public void ChangeSavedNameToInput()
    {
        SaveProperty("playerName", NameInputField.text);
        NameInputField.text = "";
        RefreshDisplayedName();
    }

    // GUI Scale
    public void RefreshGUIScale()
    {
        float GUIScale = float.Parse(LoadProperty("GUIScale"));

        MyCanvasScaler.scaleFactor = GUIScale;
    }

    public void ChangeGUIScaleToSliderValue()
    {
        float newGUIScale = GUIScaleSlider.value;

        SaveProperty("GUIScale", newGUIScale.ToString());
    }

    // Helpers
    private void SaveProperty(string property, string newValue)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + property + ".dat");

        bf.Serialize(file, newValue);
        file.Close();
    }

    private string LoadProperty(string property)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + property + ".dat", FileMode.Open);
        string savedName = (string)bf.Deserialize(file);
        file.Close();

        return savedName;        
    }
}
