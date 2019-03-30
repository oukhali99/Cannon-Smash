using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private TextMeshProUGUI speedText;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 input;
    private SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        maxSpeed += rb.drag;
        input = new Vector2(0, 0);
    }

    private void FixedUpdate()
    {
        input.x = 0;
        input.y = 0;
        anim.SetBool("isRunning", false);

        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.A))
            {
                input += Vector2.left;                            
            }
            if (Input.GetKey(KeyCode.D))
            {
                input += Vector2.right;                         
            }

            if (input.x > 0)
            {
                sr.flipX = false;
                anim.SetBool("isRunning", true);
            }
            else if (input.x < 0)
            {
                sr.flipX = true;
                anim.SetBool("isRunning", true);  
            }

            rb.AddForce(input * maxSpeed * (maxSpeed - Mathf.Abs(rb.velocity.x)));
        }

        speedText.text = "Velocity: " + (float)Mathf.RoundToInt(rb.velocity.x * 100) / 100;
    }
}
