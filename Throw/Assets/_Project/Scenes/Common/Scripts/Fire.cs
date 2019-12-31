using UnityEngine;

public class Fire : MonoBehaviour
{
    public static Fire Instance { get; private set; }

    [SerializeField] private GameObject Arrow;
    [SerializeField] private float ForceMagnitude;
    [SerializeField] private float MaxAngleHor;
    [SerializeField] private float MaxAngleVer;
    [SerializeField] private float MaxHeight;
    [SerializeField] private float MinHeight;
    [SerializeField] private float Period;
    [SerializeField] private float Cooldown;
    [SerializeField] private AudioSource NoAmmoSound;
    
    private float lastFire;
    private int state;
    private float heightPhase;
    private float verticalPhase;
    private float horizontalPhase;

    void Awake()
    {
        Instance = this;
        heightPhase = 0;
        verticalPhase = 0;
        horizontalPhase = 0;
        lastFire = -Cooldown;
        Aim();
        HeightPoint(heightPhase);
        VerticalPoint(verticalPhase);
        HorizontalPoint(horizontalPhase);
    }
	
	void Update ()
    {
        if (Ammo.Instance.ammo == 0 && Time.timeScale != 0)
        {
            Arrow.SetActive(false);
        }

        if (state == 2 || Ammo.Instance.ammo == -1)
        {
            if (Arrow.activeInHierarchy)
            {
                horizontalPhase += Time.deltaTime;
                float periodFraction = (horizontalPhase % Period) / Period;

                // Angle
                HorizontalPoint(periodFraction);
            }

            if (Ammo.Instance.ammo < 1)
            {
                Arrow.SetActive(false);
            }
            if (Press() && Time.time - lastFire > Cooldown)
            {
                Ball newBall = BallPooler.Instance.GetBall();

                if (newBall != null)
                {
                    Rigidbody newBallRigidbody = newBall.Rigidbody;

                    Ammo.Instance.PlayerFires();
                    lastFire = Time.time;
                    Vector3 forceUnitDir = Arrow.transform.up.normalized;
                    Vector3 force = forceUnitDir * ForceMagnitude;


                    newBall.transform.position = BallPooler.Instance.transform.position;
                    newBallRigidbody.velocity = Vector3.zero;
                    newBallRigidbody.AddForce(force);

                    // Ammo Wheel refresh
                    AmmoWheel.Instance.Refresh();
                }
                else
                {
                    lastFire = Time.time;
                    NoAmmoSound.Play();
                    GameOverChecker.Instance.PressedSpace();
                }
            }
        }
        else if (Arrow.activeInHierarchy && state == 1)
        {
            verticalPhase += Time.deltaTime;
            float periodFraction = (verticalPhase % Period) / Period;

            // Angle
            VerticalPoint(periodFraction);

            if (Press())
            {
                state++;
            }
        }
        else if (Arrow.activeInHierarchy && state == 0)
        {
            heightPhase += Time.deltaTime;
            float periodFraction = (heightPhase % Period) / Period;

            // Height
            HeightPoint(periodFraction);

            if (Press())
            {
                state++;
            }
        }
    }

    public void Aim()
    {
        if (state != 0) state--;
        horizontalPhase = 0;
        HorizontalPoint(horizontalPhase);
    }

    // Helpers
    private bool Press()
    {
        if (Input.touchCount != 0 && Time.timeScale != 0)
        {
            Touch lastTouch = Input.touches[Input.touchCount - 1];

            return lastTouch.phase == TouchPhase.Began;
        }
        else if (Input.GetButtonDown("Jump") && Time.timeScale != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HorizontalPoint(float periodFraction)
    {
        float newAngleY = MaxAngleHor * Mathf.Sin(periodFraction * 2 * Mathf.PI);
        Vector3 oldEulerAngles = Arrow.transform.eulerAngles;
        Arrow.transform.eulerAngles = new Vector3(oldEulerAngles.x, newAngleY, oldEulerAngles.z);
    }
    private void VerticalPoint(float periodFraction)
    {
        float oldAngleX = Arrow.transform.eulerAngles.x;
        float newAngleX = MaxAngleHor + MaxAngleHor * Mathf.Sin(periodFraction * 2 * Mathf.PI);
        float deltaAngleX = newAngleX - oldAngleX;
        Arrow.transform.Rotate(deltaAngleX, 0, 0, Space.Self);
    }
    private void HeightPoint(float periodFraction)
    {
        float oldHeight = transform.position.y;
        float newHeight = (MinHeight + MaxHeight) / 2 + ((MaxHeight - MinHeight) / 2) * Mathf.Sin(periodFraction * 2 * Mathf.PI);
        float deltaHeight = newHeight - oldHeight;
        transform.Translate(0, deltaHeight, 0);
    }
}
