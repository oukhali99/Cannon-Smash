using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPooler : MonoBehaviour
{
    public static BallPooler Instance { get; private set; }

    private LinkedList<Ball> ballPouch;

    void Awake()
    {
        Instance = this;
        ballPouch = new LinkedList<Ball>();
    }

    public Ball GetBall()
    {
        Ball ball = ballPouch.First.Value;

        ballPouch.RemoveFirst();

        return ball;
    }

    public void AddBall(Ball ball)
    {
        GameObject ballObject = Instantiate(ball.gameObject);
        Ball ballScript = ballObject.GetComponent<Ball>();
        ballPouch.AddFirst(ballScript);
    }
}

