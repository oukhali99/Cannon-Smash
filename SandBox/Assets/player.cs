using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementMultiplier;

    private Vector3 movement;
    private Rigidbody rb;
    private Camera cam;

	// Use this for initialization
	void Start ()
    {
        movement = new Vector3(0, 0, 0);
        rb = gameObject.GetComponent<Rigidbody>();
        cam = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        movement.Set(0, 0, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            movement.y += 1;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movement.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.z -= 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement.z += 1;
        }
        
        rb.AddForce(movement * movementMultiplier);

        
        //Input.mousePosition
	}
}
