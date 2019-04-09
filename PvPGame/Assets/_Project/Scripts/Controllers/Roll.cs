using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    [SerializeField] private float maxTilt;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        maxTilt = Mathf.Abs(maxTilt);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        if (Input.GetButtonUp("Fire3"))
        {
            
        }
        if (!Input.GetButton("Fire3"))
        {
            if (transform.rotation.eulerAngles.z < maxTilt || transform.rotation.eulerAngles.z > 360 - maxTilt)
            {                
                transform.rotation = Quaternion.identity;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}
