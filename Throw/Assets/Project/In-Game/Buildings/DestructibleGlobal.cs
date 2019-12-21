using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleGlobal : MonoBehaviour
{
    public static DestructibleGlobal Instance;

    public float fallVelocityThresholdSqr;
    public float ballVelocityThresholdSqr;
    public float breakWait;

    void Awake()
    {
        Instance = this;
    }
}
