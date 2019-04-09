using UnityEngine;

public class Exit : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = Global.Instance.Player;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            Global.Instance.NextLevel();
        }
    }
}
