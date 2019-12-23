using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Pooled
{
    public Rigidbody Rigidbody { get; private set; }

    // Setters
    public override GameObject Instantiate()
    {
        GameObject instantiated = GameObject.Instantiate(gameObject);
        instantiated.GetComponent<Ball>().Rigidbody = instantiated.GetComponent<Rigidbody>();

        return instantiated;
    }
}
