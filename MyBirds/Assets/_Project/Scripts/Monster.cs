using System;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    public float collisionForceToKill;

    private float collisionForceToKillSqr;
    private Ball ballScript;
    private bool readyTodie;

    private void Start()
    {
        collisionForceToKillSqr = collisionForceToKill * collisionForceToKill;
        ballScript = References.Player_Ball.GetComponent<Ball>();
        readyTodie = false;

        ballScript.BallThrown += this.EnableReadyToDie;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (readyTodie && collision.relativeVelocity.sqrMagnitude > collisionForceToKillSqr)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator<WaitForSeconds> Die()
    {
        GameObject explosion = (GameObject)Instantiate(References.Prefab_Explosion, transform.position, Quaternion.identity, null);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(10f);
        explosion.SetActive(false);
    }

    private void EnableReadyToDie(object source, EventArgs args)
    {
        readyTodie = true;
        ballScript.BallThrown -= this.EnableReadyToDie;
    }
}
