using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SmartCanvasThemer : CanvasThemer
{
    [SerializeField] private float HeightFactor;

    // Start is called before the first frame update
    void Start()
    {
        ScaleUIToHeightFactor(SceneCamera, CanvasScaler, HeightFactor);
        Themeify();
    }
}
