using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private Pooler BallPooler;
    [SerializeField] private GameObject Arrow;
    [SerializeField] private float ForceMagnitude;
    [SerializeField] private float MaxAngleHor;
    [SerializeField] private float MaxAngleVer;
    [SerializeField] private float MaxHeight;
    [SerializeField] private float MinHeight;
    [SerializeField] private float Period;
    [SerializeField] private float Cooldown;
    
    private float lastFire;
    private float verTimestamp;
    private float heightTimestamp;

	// Use this for initialization
	void Start ()
    {
        lastFire = -Cooldown;
        verTimestamp = 0;
        heightTimestamp = 0;

        // Initial arrow positioning
        InitialArrowPoint();
    }
	
	void Update ()
    {
        if (Arrow.activeInHierarchy && verTimestamp != 0 && heightTimestamp != 0)
        {
            float periodFraction = ((Time.time - verTimestamp) % Period) / Period;

            // Angle
            float newAngleY = MaxAngleHor * Mathf.Sin(periodFraction * 2 * Mathf.PI);
            Vector3 oldEulerAngles = Arrow.transform.eulerAngles;
            Arrow.transform.eulerAngles = new Vector3(oldEulerAngles.x, newAngleY, oldEulerAngles.z);

            if (Ammo.Instance.ammo == 0)
            {
                Arrow.SetActive(false);
            }
            else if (Press() && Time.time - lastFire > Cooldown)
            {
                Ammo.Instance.PlayerFires();
                lastFire = Time.time;
                Vector3 forceUnitDir = Arrow.transform.up.normalized;
                Vector3 force = forceUnitDir * ForceMagnitude;

                Ball newBall = (Ball)BallPooler.GetObject();
                Rigidbody newBallRigidbody = newBall.Rigidbody;
                newBall.transform.position = BallPooler.transform.position;
                newBallRigidbody.velocity = Vector3.zero;
                newBallRigidbody.AddForce(force);
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

    // Helpers
    private bool Press()
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

    private void InitialArrowPoint()
    {
        float oldAngleY = Arrow.transform.eulerAngles.z;
        float newAngleY = 0;
        float deltaAngleY = newAngleY - oldAngleY;
        Arrow.transform.Rotate(0, 0, deltaAngleY, Space.Self);
        float oldAngleX = Arrow.transform.eulerAngles.x;
        float newAngleX = MaxAngleHor;
        float deltaAngleX = newAngleX - oldAngleX;
        Arrow.transform.Rotate(deltaAngleX, 0, 0, Space.Self);
    }
}
