using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRightPanel : MonoBehaviour
{
    public static TopRightPanel Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void Aim()
    {
        Fire.Instance.Aim();
    }

    public void FireSignal()
    {
        Fire.Instance.FireSignal();
    }

    public void Pause()
    {
        Static.Pause();
    }
}
