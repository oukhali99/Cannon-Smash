using System;
using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Transform target;
    public float waitBetweenTargets = 3f;

    private GameObject playerBall;

    private void Start()
    {
        playerBall = References.Player_Ball;

        playerBall.GetComponent<Ball>().BallThrown += this.LookAtBall;
        playerBall.GetComponent<Ball>().BallOut += this.LookAtObstacles;
        playerBall.GetComponent<Ball>().AllOut += this.LookAtCatapult;
    }

    void FixedUpdate ()
    {
		if (target != null)
        {
            Move();
            Look();
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
        StartCoroutine(LookAtBall());
    }
    private IEnumerator LookAtBall()
    {
        yield return new WaitForSecondsRealtime(0);
        this.target = References.Player_Ball.transform;
        playerBall.GetComponent<Ball>().BallThrown -= this.LookAtBall;
    }

    private void LookAtObstacles(object source, EventArgs args)
    {
        StartCoroutine(LookAtObstacles());
    }
    private IEnumerator LookAtObstacles()
    {
        yield return new WaitForSecondsRealtime(waitBetweenTargets);
        this.target = References.Group_Obstacles.transform;
        this.offset = new Vector3(1.42f, 8.6f, -13.21f);
        playerBall.GetComponent<Ball>().BallOut -= this.LookAtObstacles;
    }

    private void LookAtCatapult(object source, EventArgs args)
    {
        // Problem: Cannot make this script wait until the end of one coroutine to start another
        // Solution: Use a Queue In Update to EnQueue any camera movement then Dequeue every fixed amount oftime
        // Signing off, Goodnight
    }
    private IEnumerator LookAtCatapult()
    {
        yield return new WaitForSecondsRealtime(waitBetweenTargets);
        this.target = References.Catapult_FrontArm.transform.parent;
        this.offset = new Vector3(4.84f, 3.1f, -3.79f);
        playerBall.GetComponent<Ball>().AllOut -= this.LookAtCatapult;
    }
}
