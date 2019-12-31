using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI Level1Percent;
    [SerializeField] private TMPro.TextMeshProUGUI Level2Percent;
    [SerializeField] private TMPro.TextMeshProUGUI Level3Percent;
    [SerializeField] private TMPro.TextMeshProUGUI Level4Percent;
    [SerializeField] private TMPro.TextMeshProUGUI Level5Percent;
    [SerializeField] private TMPro.TextMeshProUGUI Level6Percent;

    void Start()
    {
        RefreshLevelPercents();
    }

    // Helpers
    private void RefreshLevelPercents()
    {
        RefreshLevelPercent("Level1", Level1Percent);
        RefreshLevelPercent("Level2", Level2Percent);
        RefreshLevelPercent("Level3", Level3Percent);
        RefreshLevelPercent("Level4", Level4Percent);
        RefreshLevelPercent("Level5", Level5Percent);
        RefreshLevelPercent("Level6", Level6Percent);
    }
    private void RefreshLevelPercent(string levelName, TMPro.TextMeshProUGUI highScoreText)
    {
        int levelScore = SaveManager.Instance.LoadScore(levelName);
        int levelMaxScore = SaveManager.Instance.LoadMaxScore(levelName);

        highScoreText.text = Static.GetPercentageString(levelScore, levelMaxScore);
    }
}
