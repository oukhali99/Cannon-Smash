using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private string MusicPath;
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private int MusicCount;

    void Awake()
    {
        string path = MusicPath + Random.Range(0, MusicCount);
        AudioClip clip = Resources.Load<AudioClip>(path);
        
        MusicSource.clip = clip;
        MusicSource.Play();
    }
}
