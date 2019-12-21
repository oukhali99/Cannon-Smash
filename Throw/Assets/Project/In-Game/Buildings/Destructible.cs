using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public AudioSource ImpactSoundAudioSource;

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
        InGame.Instance.RefreshUI();
    }

    void Update()
    {
        if (Time.time - scoredTimestamp > DestructibleGlobal.Instance.breakWait && scored)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!scored)
        {
            if (collision.gameObject.tag.Equals("Player")
                && collision.relativeVelocity.sqrMagnitude > DestructibleGlobal.Instance.ballVelocityThresholdSqr)
            {
                Score();
            }
            else if (collision.relativeVelocity.sqrMagnitude > DestructibleGlobal.Instance.fallVelocityThresholdSqr)
            {
                Score();
            }
        }
    }

    void Score()
    {
        scored = true;
        InGame.Instance.Score++;
        InGame.Instance.RefreshUI();
        scoredTimestamp = Time.time;
        ImpactSoundAudioSource.Play();
    }
}
