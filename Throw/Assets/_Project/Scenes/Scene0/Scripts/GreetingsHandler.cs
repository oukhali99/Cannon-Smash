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
        Refresh();
    }

    // Setters
    public void ClickedChangeNameButton()
    {
        SaveManager.Instance.SavePlayerName(PlayerNameInput.text);
    }

    // Helpers
    private void Refresh()
    {
        string savedName = SaveManager.Instance.LoadPlayerName();
        GreetingsText.text = "Welcome " + savedName;
    }
}
