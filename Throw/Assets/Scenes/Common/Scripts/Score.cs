using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI Text;

    public int score { get; private set; }
    public int maxScore { get; private set; }

    void Awake()
    {
        score = 0;
        maxScore = 0;
        Instance = this;
    }

    void Start()
    {
        RefreshText();
    }

    // Setters
    public void NewDestructible()
    {
        maxScore++;
        RefreshText();
    }
    public void PlayerScores()
    {
        score++;
        RefreshText();
    }

    // Getters

    // Helpers
    private void RefreshText()
    {
        Text.text = Static.GetPercentageString(score, maxScore);
    }
}
