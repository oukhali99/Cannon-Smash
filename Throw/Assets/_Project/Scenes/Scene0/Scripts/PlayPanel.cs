using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI Level1Percent;

    void Start()
    {
        RefreshLevelPercents();
    }

    // Helpers
    private void RefreshLevelPercents()
    {
        RefreshLevelPercent("Level1", Level1Percent);
    }
    private void RefreshLevelPercent(string levelName, TMPro.TextMeshProUGUI highScoreText)
    {
        int levelScore = SaveManager.Instance.LoadScore(levelName);
        int levelMaxScore = SaveManager.Instance.LoadMaxScore(levelName);

        highScoreText.text = Static.GetPercentageString(levelScore, levelMaxScore);
    }
}
