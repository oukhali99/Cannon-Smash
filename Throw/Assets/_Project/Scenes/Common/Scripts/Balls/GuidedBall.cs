using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBall : Ball
{
    [SerializeField] private float GuidedTime;
    [SerializeField] private Vector3 CameraBallRelativePosition;
    [SerializeField] private float SlowMotionTimescale;
    [SerializeField] private float SlowdownTime;
    [SerializeField] private float XSpeed;
    [SerializeField] private float XVelocityDividePerSecond;
    [SerializeField] private AudioSource BulletTimeAudio;
    
    private static Vector3 cameraInitialPosition;
    private static Quaternion cameraInitialRotation;
    private static Camera cam;
    private static Transform cameraTransform;

    private float firedTimestamp;
    private Camera myCamera;
    private new Transform transform;
    private float XViewportTouchDownPosition;
    private float slowdownPerSecond;

    void Start()
    {
        transform = gameObject.transform;
        firedTimestamp = 0;
        slowdownPerSecond = (1 - SlowMotionTimescale) / SlowdownTime;

        GetCamera();
    }

    void Update()
    {
        if (firedTimestamp != 0)
        {
            CameraFollows();
            FollowFinger();

            if (Time.time - firedTimestamp > GuidedTime)
            {
                Speedup();
            }
            else
            {
                Slowdown();
            }
        }
    }

    override public void Fired()
    {
        BulletTimeAudio.Play();
        firedTimestamp = Time.time;
        TopRightPanel.Instance.gameObject.SetActive(false);
        GameOverChecker.Instance.PressedSpace();

        GetCamera();
        cameraInitialPosition = cameraTransform.position;
        cameraInitialRotation = cameraTransform.rotation;
    }

    private void DoneFiring()
    {
        firedTimestamp = 0;
        
        TopRightPanel.Instance.gameObject.SetActive(true);
        cameraTransform.position = cameraInitialPosition;
        
        cameraTransform.position = cameraInitialPosition;
        cameraTransform.rotation = cameraInitialRotation;

        GameOverChecker.Instance.PressedSpace();
        firedTimestamp = 0;
    }

    private void FollowFinger()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            Vector3 touchPosition = touch.position;
            Vector3 position = transform.position;
            touchPosition.z = position.z;

            Vector3 touchViewportdPosition = cam.ScreenToViewportPoint(touchPosition);

            if (touch.phase == TouchPhase.Began)
            {
                XViewportTouchDownPosition = touchViewportdPosition.x;
            }

            float xVelocityUnit = 2 * (touchViewportdPosition.x - XViewportTouchDownPosition);

            Vector3 velocity = Rigidbody.velocity;

            Rigidbody.velocity = Vector3.right * xVelocityUnit * XSpeed + Vector3.up * velocity.y + Vector3.forward * velocity.z;
        }
        else
        {
            Vector3 velocity = Rigidbody.velocity;

            Rigidbody.velocity = Vector3.right * velocity.x / Mathf.Pow(XVelocityDividePerSecond, Time.deltaTime) + Vector3.up * velocity.y + Vector3.forward * velocity.z;
        }
    }

    private void CameraFollows()
    {
        cameraTransform.position = transform.position + CameraBallRelativePosition;
        cameraTransform.rotation = cameraInitialRotation;
    }

    private void GetCamera()
    {
        if (cam == null)
        {
            cam = Camera.main;
            cameraTransform = cam.transform;
            cameraInitialPosition = cameraTransform.position;
            cameraInitialRotation = cameraTransform.rotation;
        }
    }

    private void Slowdown()
    {
        float predictedTimescale = Time.timeScale - slowdownPerSecond * Time.deltaTime;

        if (predictedTimescale < SlowMotionTimescale)
        {
            Time.timeScale = SlowMotionTimescale;
        }
        else
        {
            Time.timeScale = predictedTimescale;
        }
    }

    private void Speedup()
    {
        float predictedTimescale = Time.timeScale + slowdownPerSecond * Time.deltaTime;

        if (predictedTimescale > 1)
        {
            Time.timeScale = 1;
            DoneFiring();
        }
        else
        {
            Time.timeScale = predictedTimescale;
        }
    }
}
