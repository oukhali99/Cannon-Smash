using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
