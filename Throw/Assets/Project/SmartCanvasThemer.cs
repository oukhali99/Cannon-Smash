using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SmartCanvasThemer : CanvasThemer
{
    [SerializeField] private float HeightFactor;

    void Start()
    {
        RefreshUI();
        Themeify();
    }

    public new void RefreshUI()
    {
        ScaleUIToHeightFactor(SceneCamera, CanvasScaler, HeightFactor);
    }
}
