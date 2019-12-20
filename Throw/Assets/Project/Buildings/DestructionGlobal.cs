using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionGlobal : MonoBehaviour
{
    public static DestructionGlobal Instance;

    public float fallVelocityThresholdSqr;
    public float ballVelocityThresholdSqr;
    public float breakWait;

    void Awake()
    {
        Instance = this;
    }
}
