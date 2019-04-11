using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance;

    public Camera Camera;

    private void Awake()
    {
        Instance = this;
    }
}
