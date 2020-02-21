using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Ball : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public AudioSource FiredSound;

    abstract public void Fired();
}
