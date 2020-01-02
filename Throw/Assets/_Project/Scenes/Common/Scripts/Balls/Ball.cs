using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Ball : MonoBehaviour
{
    public Rigidbody Rigidbody;

    abstract public void Fired();
}
