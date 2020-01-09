using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private AudioSource ImpactSoundAudioSource;
    [SerializeField] private float FallVelocityThresholdSqr;
    [SerializeField] private float BallVelocityThresholdSqr;
    [SerializeField] private float BreakWait;
    [SerializeField] private AudioClip[] BreakSounds;

    private bool scored;
    private float scoredTimestamp;

    void Awake()
    {
        scored = false;
        scoredTimestamp = 0;
    }

    void Start()
    {
        global::Score.Instance.NewDestructible();
        ImpactSoundAudioSource.clip = BreakSounds[Random.Range(0, BreakSounds.Length)];
    }

    void Update()
    {
        if (Time.time - scoredTimestamp > BreakWait && scored)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string otherTag = collision.gameObject.tag;

        if (!scored)
        {
            if (otherTag.Equals("Player")
                && collision.relativeVelocity.sqrMagnitude > BallVelocityThresholdSqr)
            {
                Score();
            }
            else if (collision.relativeVelocity.sqrMagnitude > FallVelocityThresholdSqr)
            {
                Score();
            }
        }

        ImpactSoundAudioSource.Play();
    }


    // Helpers
    private void Score()
    {
        scored = true;
        global::Score.Instance.PlayerScores();
        scoredTimestamp = Time.time;
    }
}
