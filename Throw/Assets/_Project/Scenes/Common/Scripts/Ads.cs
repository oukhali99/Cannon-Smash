using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    private string gameId = "3415015";
    private bool testMode = true;
    private bool ready;

    void Awake()
    {
        ready = false;
    }

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    void Update()
    {
        if (!ready)
        {
            if (Advertisement.IsReady())
            {
                ready = true;
            }
        }
    }

    public void ShowAd()
    {
        if (ready)
        {
            Advertisement.Show();
        }
    }
}