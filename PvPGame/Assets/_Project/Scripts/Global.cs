using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static GameObject cam;

    public GameObject camH;

    private void Awake()
    {
        cam = camH;
    }
}
