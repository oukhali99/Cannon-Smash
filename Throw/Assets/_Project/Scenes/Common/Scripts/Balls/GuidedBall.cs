using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBall : Ball
{
    [SerializeField] private float GuidedTime;
    [SerializeField] private Vector3 CameraBallRelativePosition;
    [SerializeField] private float SlowMotionTimescale;
    [SerializeField] private float XSpeed;
    [SerializeField] private float XVelocityDividePerSecond;
    
    private static Vector3 cameraInitialPosition;
    private static Quaternion cameraInitialRotation;
    private static Camera cam;
    private static Transform cameraTransform;

    private float firedTimestamp;
    private Camera myCamera;
    private bool slowMotion;
    private new Transform transform;
    private float XViewportTouchDownPosition;

    void Start()
    {
        transform = gameObject.transform;
        firedTimestamp = 0;
        slowMotion = false;

        if (cam == null)
        {
            cam = Camera.main;
            cameraTransform = cam.transform;
            cameraInitialPosition = cameraTransform.position;
            cameraInitialRotation = cameraTransform.rotation;
        }
    }

    void Update()
    {
        FollowFinger();
        if (firedTimestamp != 0)
        {
            CameraFollows();
            FollowFinger();

            if (Time.time - firedTimestamp > GuidedTime)
            {
                DoneFiring();
            }
        }
    }

    override public void Fired()
    {
        firedTimestamp = Time.time;

        TopRightPanel.Instance.gameObject.SetActive(false);
        cameraInitialPosition = cameraTransform.position;
        cameraInitialRotation = cameraTransform.rotation;

        GameOverChecker.Instance.PressedSpace();

        if (!slowMotion)
        {
            slowMotion = true;
            Time.timeScale *= SlowMotionTimescale;
            MusicHandler.Instance.MusicSource.pitch *= SlowMotionTimescale;
        }
    }

    private void DoneFiring()
    {
        firedTimestamp = 0;

        // Go back to normal mode
        TopRightPanel.Instance.gameObject.SetActive(true);
        cameraTransform.position = cameraInitialPosition;

        // Set Transform
        cameraTransform.position = cameraInitialPosition;
        cameraTransform.rotation = cameraInitialRotation;

        GameOverChecker.Instance.PressedSpace();

        if (slowMotion)
        {
            slowMotion = false;
            Time.timeScale /= SlowMotionTimescale;
            MusicHandler.Instance.MusicSource.pitch /= SlowMotionTimescale;
        }
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
}
