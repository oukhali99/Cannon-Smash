using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public static MusicHandler Instance { get; private set; }

    public AudioSource MusicSource;

    [SerializeField] private string MusicPath;
    [SerializeField] private int MusicCount;

    void Awake()
    {
        Instance = this;

        string path = MusicPath + Random.Range(0, MusicCount);
        AudioClip clip = Resources.Load<AudioClip>(path);
        
        MusicSource.clip = clip;
        MusicSource.Play();
    }
}
