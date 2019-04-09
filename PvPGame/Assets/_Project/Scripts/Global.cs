using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public static Global Instance;

    public GameObject Cam;
    public GameObject Player;

    private void Awake()
    {
        Instance = this;
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

    public void NextLevel()
    {
        int cur;

        cur = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(cur + 1);
    }
}
