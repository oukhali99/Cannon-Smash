using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBall : Ball
{
    [SerializeField] private float VelocityMultiplier;

    override public void Fired()
    {
        Rigidbody.velocity = Rigidbody.velocity * VelocityMultiplier;
    }
}
