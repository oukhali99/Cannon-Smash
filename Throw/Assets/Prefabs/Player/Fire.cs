using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Indicator;
    public float MaxForce;
    public float MaxAngle;
    public float Period;

    private Rigidbody ballRb;
    private float angle;
    private float force;
    private float timestamp;

	// Use this for initialization
	void Start ()
    {
        ballRb = Ball.GetComponent<Rigidbody>();
        timestamp = 0;
        UpdateAngle(timestamp);
        UpdateForce(timestamp);
    }
	
	// Update is called once per frame
	void Update ()
    {
        timestamp = Time.time;
        UpdateAngle(timestamp);
        UpdateForce(timestamp);
        Indicator.transform.eulerAngles.Set(Indicator.transform.eulerAngles.x, angle, Indicator.transform.eulerAngles.z);
    }

    void UpdateAngle(float timestamp)
    {
        angle = Random.Range(0, 200);
    }

    void UpdateForce(float timestamp)
    {
        force = timestamp / Period;
    }
}
