using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSky : MonoBehaviour
{
    public Material[] Skyboxes;

    void Start()
    {
        Material chosenSkybox = Skyboxes[Mathf.FloorToInt(Random.Range(0, Skyboxes.Length))];
        RenderSettings.skybox = chosenSkybox;
    }
}
