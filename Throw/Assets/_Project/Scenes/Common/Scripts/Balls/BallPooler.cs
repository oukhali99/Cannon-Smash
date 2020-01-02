using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPooler : MonoBehaviour
{
    public static BallPooler Instance { get; private set; }

    public LinkedList<Ball> SelectedAmmo { get; set; }
    public LinkedList<Ball> NormalBallList { get; private set; }
    public LinkedList<Ball> ExplosiveBallList { get; private set; }
    public LinkedList<Ball> GuidedBallList { get; private set; }
    public LinkedList<Ball> LargeBallList { get; private set; }

    void Awake()
    {
        Instance = this;
        NormalBallList = new LinkedList<Ball>();
        ExplosiveBallList = new LinkedList<Ball>();
        GuidedBallList = new LinkedList<Ball>();
        LargeBallList = new LinkedList<Ball>();
        SelectedAmmo = NormalBallList;
    }

    public Ball GetBall()
    {
        var ball = SelectedAmmo.First;

        if (ball == null)
        {
            return null;
        }
        else
        {
            SelectedAmmo.RemoveFirst();
            return ball.Value;
        }

    }

    public void AddNormalBall(Ball ball)
    {
        GameObject ballObject = Instantiate(ball.gameObject);
        Ball ballScript = ballObject.GetComponent<Ball>();
        NormalBallList.AddFirst(ballScript);
    }
    public void AddExplosiveBall(ExplosiveBall ball)
    {
        GameObject ballObject = Instantiate(ball.gameObject);
        ExplosiveBall ballScript = ballObject.GetComponent<ExplosiveBall>();
        ExplosiveBallList.AddFirst(ballScript);
    }
    public void AddAntigravtityBall(GuidedBall ball)
    {
        GameObject ballObject = Instantiate(ball.gameObject);
        GuidedBall ballScript = ballObject.GetComponent<GuidedBall>();
        GuidedBallList.AddFirst(ballScript);
    }
    public void AddLargeBall(Ball ball)
    {
        GameObject ballObject = Instantiate(ball.gameObject);
        Ball ballScript = ballObject.GetComponent<Ball>();
        LargeBallList.AddFirst(ballScript);
    }

    public bool AllEmpty()
    {
        return NormalBallList.Count == 0 && ExplosiveBallList.Count == 0 && GuidedBallList.Count == 0 && LargeBallList.Count == 0;
    }
}

