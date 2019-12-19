using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionGlobal : MonoBehaviour
{
    public static float FallVelocityThresholdSqr;
    public static float BallVelocityThresholdSqr;

    public float fallVelocityThresholdSqr;
    public float ballVelocityThresholdSqr;
    public float breakWait;

    void Awake()
    {
        FallVelocityThresholdSqr = fallVelocityThresholdSqr;
        BallVelocityThresholdSqr = ballVelocityThresholdSqr;
    }
}
