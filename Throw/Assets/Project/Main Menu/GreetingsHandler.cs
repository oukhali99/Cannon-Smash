using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GreetingsHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerNameInput;
    [SerializeField] private TextMeshProUGUI GreetingsText;
    [SerializeField] private string PlayerNameDefault;
    
    void Start()
    {
        FirstTimeSaveCheck();
    }

    void OnEnable()
    {
        Refresh();
    }

    // Setters
    public void ClickedChangeNameButton()
    {
        PlayerPrefs.SetString("PlayerName", PlayerNameInput.text);
    }

    // Helpers
    private void FirstTimeSaveCheck()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            PlayerPrefs.SetString("PlayerName", PlayerNameDefault);
            PlayerPrefs.Save();
        }
    }
    private void Refresh()
    {
        string savedName = PlayerPrefs.GetString("PlayerName");
        GreetingsText.text = "Welcome " + savedName;
    }
}
