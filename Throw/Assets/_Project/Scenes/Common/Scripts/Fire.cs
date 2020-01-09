using UnityEngine;

public class Fire : MonoBehaviour
{
    public static Fire Instance { get; private set; }
    
    [SerializeField] private GameObject Arrow;
    [SerializeField] private Transform ArrowTransform;
    [SerializeField] private float ForceMagnitude;
    [SerializeField] private float MaxAngleHor;
    [SerializeField] private float MaxAngleVer;
    [SerializeField] private float MaxHeight;
    [SerializeField] private float MinHeight;
    [SerializeField] private float Period;
    [SerializeField] private float Cooldown;
    [SerializeField] private AudioSource NoAmmoSound;
    [SerializeField] private Animator MyAnimator;
    [SerializeField] private Transform AimingArchTransform;
    
    private float lastFire;
    private int state;
    private float heightPhase;
    private float verticalPhase;
    private float horizontalPhase;
    private bool fireSignal;
    private Vector3 cameraPosition;
    private Quaternion cameraRotation;

    void Awake()
    {
        fireSignal = false;
        Instance = this;
        heightPhase = 0;
        verticalPhase = 0;
        horizontalPhase = 0;
        lastFire = -Cooldown;        
    }
	
    void Start()
    {
        Aim();
        HeightPoint(heightPhase);
        VerticalPoint(verticalPhase);
        HorizontalPoint(horizontalPhase);

        Transform cameraTransform = Camera.main.transform;
        cameraPosition = cameraTransform.position;
        cameraRotation = cameraTransform.rotation;
    }

	void Update ()
    {
        if (Ammo.Instance.ammo == 0 && Time.timeScale != 0)
        {
            Arrow.SetActive(false);
        }

        if (state == 3 || Ammo.Instance.ammo == -1)
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
            if (fireSignal && Time.time - lastFire > Cooldown)
            {
                Ball newBall = BallPooler.Instance.GetBall();

                if (newBall != null)
                {
                    Rigidbody newBallRigidbody = newBall.Rigidbody;
                    Vector3 forceUnitDir = ArrowTransform.up.normalized;
                    Vector3 force = forceUnitDir * ForceMagnitude;

                    Ammo.Instance.PlayerFires();
                    lastFire = Time.time;
                    
                    newBall.transform.position = BallPooler.Instance.transform.position;
                    newBallRigidbody.velocity = Vector3.zero;
                    newBallRigidbody.AddForce(force, ForceMode.Acceleration);
                    newBall.FiredSound.Play();
                    newBall.Fired();
                    
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
        else if (Arrow.activeInHierarchy && state == 2)
        {
            verticalPhase += Time.deltaTime;
            float periodFraction = (verticalPhase % Period) / Period;

            // Angle
            VerticalPoint(periodFraction);

            if (fireSignal)
            {
                state++;
            }
        }
        else if (Arrow.activeInHierarchy && state == 1)
        {
            heightPhase += Time.deltaTime;
            float periodFraction = (heightPhase % Period) / Period;

            // Height
            HeightPoint(periodFraction);

            if (fireSignal)
            {
                state++;
            }
        }
        else if (state == 0)
        {
            if (fireSignal)
            {
                state++;
                MyAnimator.enabled = false;

                Transform camera = Camera.main.transform;
                camera.transform.position = cameraPosition;
                camera.transform.rotation = cameraRotation;
            }
        }

        RefreshAimingArch();

        fireSignal = false;
    }

    public void FireSignal()
    {
        fireSignal = true;
    }

    public void Aim()
    {
        if (state > 1) state--;
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
        Vector3 oldEulerAngles = ArrowTransform.eulerAngles;
        ArrowTransform.eulerAngles = new Vector3(oldEulerAngles.x, newAngleY, oldEulerAngles.z);
    }
    private void VerticalPoint(float periodFraction)
    {
        float oldAngleX = ArrowTransform.eulerAngles.x;
        float newAngleX = MaxAngleHor + MaxAngleHor * Mathf.Sin(periodFraction * 2 * Mathf.PI);
        float deltaAngleX = newAngleX - oldAngleX;
        ArrowTransform.Rotate(deltaAngleX, 0, 0, Space.Self);
    }
    private void HeightPoint(float periodFraction)
    {
        float oldHeight = transform.position.y;
        float newHeight = (MinHeight + MaxHeight) / 2 + ((MaxHeight - MinHeight) / 2) * Mathf.Sin(periodFraction * 2 * Mathf.PI);
        float deltaHeight = newHeight - oldHeight;
        transform.Translate(0, deltaHeight, 0);
    }

    private void RefreshAimingArch()
    {
    }
}
