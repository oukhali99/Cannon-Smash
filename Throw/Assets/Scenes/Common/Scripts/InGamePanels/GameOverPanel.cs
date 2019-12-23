using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public static GameOverPanel Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Unpause()
    {
        Static.Unpause();
    }

    public void LoadScene(int index)
    {
        Static.LoadScene(index);
    }
}
