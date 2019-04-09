using UnityEngine;

public class Global : MonoBehaviour
{
    public static GameObject cam;
    public static GameObject Player;

    public GameObject camH;
    public GameObject PlayerH;

    private void Awake()
    {
        cam = camH;
        Player = PlayerH;
    }

    public static float exponent(float num, int exp)
    {
        if (exp == 0)
        {
            return 1;
        }
        if (exp == 1)
        {
            return num;
        }

        float halfExp = exponent(num, exp / 2);

        if (exp % 2 == 0)
        {
            return halfExp * halfExp;
        }
        else
        {
            return num * halfExp * halfExp;
        }
    }
}
