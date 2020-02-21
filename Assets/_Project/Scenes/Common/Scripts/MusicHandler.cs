using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public static MusicHandler Instance { get; private set; }

    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource ScratchSource;
    [SerializeField] private AudioSource LevelCompleteSource;
    [SerializeField] private string MusicPath;
    [SerializeField] private int MusicCount;

    private AudioSource[] allAudioSources;
    private float playedScratchTimestamp;
    private float scratchLength;

    void Awake()
    {
        playedScratchTimestamp = 0;
        scratchLength = ScratchSource.clip.length;
        Instance = this;

        string path = MusicPath + Random.Range(0, MusicCount);
        AudioClip clip = Resources.Load<AudioClip>(path);
        
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
        if (Time.timeScale != 0) SetAllAudioSourcesPitch(Time.timeScale);
        if (playedScratchTimestamp != 0 && Time.time - playedScratchTimestamp > scratchLength)
        {
            playedScratchTimestamp = 0;
            LevelCompleteSource.Play();
        }
    }

    public void SetAllAudioSourcesPitch(float newPitch)
    {
        foreach (AudioSource cur in allAudioSources)
        {
            cur.pitch = newPitch;
        }
    }

    public void LevelComplete()
    {
        ScratchSource.Play();
        MusicSource.Stop();
        playedScratchTimestamp = Time.time;
    }
}
