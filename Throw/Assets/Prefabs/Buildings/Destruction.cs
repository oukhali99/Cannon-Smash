using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    private bool scored;

    void Awake()
    {
        Global.MaxScore++;
        scored = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!scored)
        {
            if (collision.gameObject.tag.Equals("Player")
                && collision.relativeVelocity.sqrMagnitude > DestructionGlobal.BallVelocityThresholdSqr)
            {
                Score();
            }
            else if (collision.relativeVelocity.sqrMagnitude > DestructionGlobal.FallVelocityThresholdSqr)
            {
                Score();
            }
        }
    }

    void Score()
    {
        scored = true;
        Global.Score++;
        Global.UpdateScoreboard();
    }
}
