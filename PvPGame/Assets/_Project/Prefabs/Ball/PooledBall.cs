using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledBall : MonoBehaviour, IPooledObject
{
    [SerializeField] private float xMax = .1f;
    [SerializeField] private float yMax = .1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-xMax, xMax);
        float yForce = Random.Range(-yMax, yMax);

        rb.velocity = new Vector2(xForce, yForce);
        rb.angularVelocity = 0f;
    }
}
