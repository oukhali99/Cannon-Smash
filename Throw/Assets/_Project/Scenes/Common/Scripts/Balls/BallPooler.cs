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

    [SerializeField] private Vector3 DisplayPosition;

    private new Transform transform;

    void Awake()
    {
        Instance = this;
        NormalBallList = new LinkedList<Ball>();
        ExplosiveBallList = new LinkedList<Ball>();
        GuidedBallList = new LinkedList<Ball>();
        LargeBallList = new LinkedList<Ball>();
        SelectedAmmo = NormalBallList;
        transform = gameObject.transform;
    }

    void Update()
    {
        if (SelectedAmmo.Count > 1)
        {
            var firstNode = SelectedAmmo.Last;

            if (firstNode != null)
            {
                firstNode.Value.transform.position = DisplayPosition + transform.position;
            }
        }
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
            ball.Value.gameObject.SetActive(true);

            return ball.Value;
        }

    }

    public void AddNormalBall(NormalBall ball)
    {
        GameObject ballObject = Instantiate(ball.gameObject);
        NormalBall ballScript = ballObject.GetComponent<NormalBall>();
        Transform ballTransform = ballObject.transform;

        NormalBallList.AddFirst(ballScript);
        ballTransform.position = new Vector3(0, 0, -100);
    }
    public void AddExplosiveBall(ExplosiveBall ball)
    {
        GameObject ballObject = Instantiate(ball.gameObject);
        ExplosiveBall ballScript = ballObject.GetComponent<ExplosiveBall>();
        Transform ballTransform = ballObject.transform;

        ExplosiveBallList.AddFirst(ballScript);
        ballObject.SetActive(false);
        ballTransform.position = new Vector3(0, 0, -100);
    }
    public void AddAntigravtityBall(GuidedBall ball)
    {
        GameObject ballObject = Instantiate(ball.gameObject);
        GuidedBall ballScript = ballObject.GetComponent<GuidedBall>();
        Transform ballTransform = ballObject.transform;

        GuidedBallList.AddFirst(ballScript);
        ballObject.SetActive(false);

        ballTransform.position = new Vector3(0, 0, -100);
    }
    public void AddLargeBall(LargeBall ball)
    {
        GameObject ballObject = Instantiate(ball.gameObject);
        LargeBall ballScript = ballObject.GetComponent<LargeBall>();
        Transform ballTransform = ballObject.transform;

        LargeBallList.AddFirst(ballScript);
        ballTransform.position = new Vector3(0, 0, -100);
    }

    public bool AllEmpty()
    {
        return NormalBallList.Count == 0 && ExplosiveBallList.Count == 0 && GuidedBallList.Count == 0 && LargeBallList.Count == 0;
    }
}

