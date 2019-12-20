using UnityEngine;

public class Fire : MonoBehaviour
{
    public Pooler BallPooler;
    public GameObject Arrow;
    public float MaxForceMag;
    public float MaxAngleHor;
    public float MaxAngleVer;
    public float MaxHeight;
    public float MinHeight;
    public float Period;
    public float Cooldown;

    private Rigidbody[] ballRbs;
    private Vector3 firePos;
    private float lastFire;
    private float verTimestamp;
    private float heightTimestamp;

	// Use this for initialization
	void Start ()
    {
        ballRbs = new Rigidbody[BallPooler.Pool.Length];

        for (int i = 0; i < BallPooler.Pool.Length; i++)
        {
            ballRbs[i] = BallPooler.Pool[i].GetComponent<Rigidbody>();
        }

        firePos = BallPooler.transform.position;
        lastFire = -Cooldown;
        verTimestamp = 0;
        heightTimestamp = 0;

        // Initial arrow positioning
        float oldAngleY = Arrow.transform.eulerAngles.z;
        float newAngleY = 0;
        float deltaAngleY = newAngleY - oldAngleY;
        Arrow.transform.Rotate(0, 0, deltaAngleY, Space.Self);
        float oldAngleX = Arrow.transform.eulerAngles.x;
        float newAngleX = MaxAngleHor;
        float deltaAngleX = newAngleX - oldAngleX;
        Arrow.transform.Rotate(deltaAngleX, 0, 0, Space.Self);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Arrow.activeInHierarchy && verTimestamp != 0 && heightTimestamp != 0)
        {
            float periodFraction = ((Time.time - verTimestamp) % Period) / Period;

            // Angle
            float oldAngleY = Arrow.transform.eulerAngles.z;
            float newAngleY = MaxAngleHor * Mathf.Sin(periodFraction * 2 * Mathf.PI) + 1;
            float deltaAngleY = newAngleY - oldAngleY;
            Arrow.transform.Rotate(0, 0, deltaAngleY, Space.Self);

            // Force
            float forceMag = MaxForceMag;

            if (InGame.Instance.Ammo == 0)
            {
                Arrow.SetActive(false);
            }
            else if (Press() && Time.time - lastFire > Cooldown)
            {
                InGame.Instance.Ammo--;
                InGame.Instance.UpdateScoreboard();
                lastFire = Time.time;
                Vector3 forceUnitDir = Arrow.transform.up.normalized;
                Vector3 force = forceUnitDir * forceMag;

                int newBallIndex = BallPooler.GetObjectIndex();
                GameObject newBall = BallPooler.Pool[newBallIndex];
                newBall.transform.position = BallPooler.transform.position;
                ballRbs[newBallIndex].velocity = Vector3.zero;
                ballRbs[newBallIndex].AddForce(force);
            }
        }
        else if (Arrow.activeInHierarchy && heightTimestamp != 0)
        {
            float periodFraction = (Time.time % Period - heightTimestamp) / Period;

            // Angle
            float oldAngleX = Arrow.transform.eulerAngles.x;
            float newAngleX = MaxAngleHor + MaxAngleHor * Mathf.Sin(periodFraction * 2 * Mathf.PI);
            float deltaAngleX = newAngleX - oldAngleX;
            Arrow.transform.Rotate(deltaAngleX, 0, 0, Space.Self);

            if (Press())
            {
                verTimestamp = Time.time;
            }
        }
        else if (Arrow.activeInHierarchy)
        {
            float periodFraction = (Time.time % Period) / Period;

            // Height
            float oldHeight = transform.position.y;
            float newHeight = (MinHeight + MaxHeight) / 2 + ((MaxHeight - MinHeight) / 2) * Mathf.Sin(periodFraction * 2 * Mathf.PI);
            float deltaHeight = newHeight - oldHeight;
            transform.Translate(0, deltaHeight, 0);

            if (Press())
            {
                heightTimestamp = Time.time;
            }
        }
    }

    bool Press()
    {
        if (Input.touchCount != 0)
        {
            Touch lastTouch = Input.touches[Input.touchCount - 1];

            return lastTouch.phase == TouchPhase.Began;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
