using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    
    Vector2 move;
    Vector2 curPos2D;
    Rigidbody2D rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), 0);
        curPos2D = transform.position;

        rb.MovePosition(curPos2D + move * speed);
    }
}
