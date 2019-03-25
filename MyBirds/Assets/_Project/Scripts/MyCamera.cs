using System;
using UnityEngine;
using System.Collections.Generic;

public class MyCamera : MonoBehaviour {

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Transform target;
    public float waitBetweenTargets = 3f;

    private GameObject playerBall;
    private Ball playerBallScript;
    private Queue<GameObject> targets;
    private Queue<Vector3> offsets;
    private float lastQueueOperation;

    private void Start()
    {
        playerBall = References.Player_Ball;
        playerBallScript = playerBall.GetComponent<Ball>();
        targets = new Queue<GameObject>();
        offsets = new Queue<Vector3>();
        lastQueueOperation = -waitBetweenTargets;

        // subscribe
        playerBallScript.BallThrown += this.LookAtBall;
        playerBallScript.BallOut += this.LookAtObstacles;
        playerBallScript.AllOut += this.LookAtCatapult;
    }

    void FixedUpdate ()
    {
		if (target != null)
        {
            Move();
            Look();
        }

        if (targets.Count > 0 && Time.realtimeSinceStartup - lastQueueOperation > waitBetweenTargets)
        {
            target = targets.Dequeue().transform;
            offset = offsets.Dequeue();
            lastQueueOperation = Time.realtimeSinceStartup;
        }
	}

    void Move()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

        transform.position = smoothedPos;
    }

    void Look()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed);
    }

    private void LookAtBall(object source, EventArgs args)
    {
        targets.Enqueue(References.Player_Ball);
        offsets.Enqueue(offset);
        playerBallScript.BallThrown -= LookAtBall;
    }

    private void LookAtObstacles(object source, EventArgs args)
    {
        targets.Enqueue(References.Group_Obstacles);
        offsets.Enqueue(new Vector3(1.42f, 8.6f, -13.21f));
        playerBallScript.BallOut -= LookAtObstacles;
    }

    private void LookAtCatapult(object source, EventArgs args)
    {
        // Problem: Cannot make this script wait until the end of one coroutine to start another
        // Solution: Use a Queue In Update to EnQueue any camera movement then Dequeue every fixed amount oftime
        // Signing off, Goodnight
        targets.Enqueue(References.Catapult_FrontArm.transform.parent.gameObject);
        offsets.Enqueue(new Vector3(4.84f, 3.1f, -3.79f));
        playerBallScript.AllOut -= LookAtCatapult;
    }
}
