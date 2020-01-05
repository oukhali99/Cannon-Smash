using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBall : Ball
{
    [SerializeField] private float GuidedTime;
    [SerializeField] private Vector3 CameraBallRelativePosition;
    [SerializeField] private PlayerMoveController Controller;
    [SerializeField] private float SloMoTimescale;
    
    private static Vector3 cameraInitialPosition;
    private static Quaternion cameraInitialRotation;
    private static Camera cam;

    private float firedTimestamp;
    private float fixedDeltaTimeScale;

    void Start()
    {
        firedTimestamp = 0;
        fixedDeltaTimeScale = Time.fixedDeltaTime / Time.timeScale;

        if (cam == null)
        {
            cam = Camera.main;
            cameraInitialPosition = cam.transform.position;
            cameraInitialRotation = cam.transform.rotation;
        }
    }

    void Update()
    {
        if (firedTimestamp != 0)
        {
            // Set the Transform
            cam.transform.position = transform.position + CameraBallRelativePosition;
            cam.transform.rotation = cameraInitialRotation;

            if (Time.time - firedTimestamp > GuidedTime)
            {
                DoneFiring();
            }
        }
    }

    override public void Fired()
    {
        Time.timeScale = SloMoTimescale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        firedTimestamp = Time.time;

        TopRightPanel.Instance.gameObject.SetActive(false);
        SimpleTouchController.Instance.gameObject.SetActive(true);
        Controller.leftController = SimpleTouchController.Instance;
        cameraInitialPosition = cam.transform.position;
        cameraInitialRotation = cam.transform.rotation;
    }

    private void DoneFiring()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        firedTimestamp = 0;

        // Go back to normal mode
        TopRightPanel.Instance.gameObject.SetActive(true);
        SimpleTouchController.Instance.gameObject.SetActive(false);
        Controller.leftController = null;
        cam.transform.position = cameraInitialPosition;

        // Set Transform
        cam.transform.position = cameraInitialPosition;
        cam.transform.rotation = cameraInitialRotation;
    }
}
