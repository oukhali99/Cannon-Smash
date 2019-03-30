using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float epsilon;

    private Rigidbody2D rb;

    private bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        grounded = false;
    }

    private void Update()
    {
        if (ReadyToJump() && Input.GetButton("Jump"))
        {
            grounded = false;
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
        }
    }

    private bool ReadyToJump()
    {
        if (!grounded)
        {
            return false;
        }
        if (Mathf.Abs(rb.velocity.y) >= epsilon)
        {
            return false;
        }

        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.GetContact(0).point - (Vector2)transform.position);
        grounded = true;
    }
}
