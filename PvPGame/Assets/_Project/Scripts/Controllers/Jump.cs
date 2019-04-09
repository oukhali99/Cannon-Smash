using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float niggaFactor;
    [SerializeField] private float groundedAngle;
    [SerializeField] private float fallMultiplierFloat;
    [SerializeField] private float lowJumpMultiplierFloat;

    private Rigidbody2D rb;
    private Animator anim;

    private bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grounded = false;
    }

    private void Update()
    {
        if (grounded && Input.GetButtonDown("Jump"))
        {
            grounded = false;
            rb.AddForce(Vector2.up * jumpForce + Vector2.up * Mathf.Abs(rb.velocity.x) * niggaFactor, ForceMode2D.Impulse);
        }

        if (rb.velocity.y < 0 && !grounded)
        {
            rb.velocity += Vector2.up* Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime;
            anim.SetBool("jumpDown", true);
            anim.SetBool("jumpUp", false);
        }
        else if (rb.velocity.y > 0 && !grounded)
        {
            anim.SetBool("jumpDown", false);
            anim.SetBool("jumpUp", true);
            if (!Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplierFloat - 1) * Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("jumpDown", false);
            anim.SetBool("jumpUp", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = collision.contacts;

        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 objPos = transform.position;
            Vector2 contactPos = contacts[i].point;
            float angle;

            Vector2 objToContact = contactPos - objPos;
            angle = Vector2.Angle(Vector2.right, objToContact);

            if (angle < (180 - groundedAngle) && angle > groundedAngle)
            {
                grounded = true;
                
                if (anim.isActiveAndEnabled)
                {
                    anim.SetBool("jumpDown", false);
                    anim.SetBool("jumpUp", false);
                }
            }
        }        
    }
}
