using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    private bool scored;
    private float scoredTimestamp;

    void Awake()
    {
        scored = false;
        scoredTimestamp = 0;
    }

    void Start()
    {
        InGame.Instance.MaxScore++;
    }

    void Update()
    {
        if (Time.time - scoredTimestamp > DestructionGlobal.Instance.breakWait && scored)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!scored)
        {
            if (collision.gameObject.tag.Equals("Player")
                && collision.relativeVelocity.sqrMagnitude > DestructionGlobal.Instance.ballVelocityThresholdSqr)
            {
                Score();
            }
            else if (collision.relativeVelocity.sqrMagnitude > DestructionGlobal.Instance.fallVelocityThresholdSqr)
            {
                Score();
            }
        }
    }

    void Score()
    {
        scored = true;
        InGame.Instance.Score++;
        InGame.Instance.UpdateScoreboard();
        scoredTimestamp = Time.time;
    }
}
