using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        Global.Ammo++;
	}

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
