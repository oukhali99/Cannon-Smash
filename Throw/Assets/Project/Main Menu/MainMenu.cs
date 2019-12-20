using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ReleadScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        LoadScene(index);
    }

    public void ChangeName()
    {

    }
}
