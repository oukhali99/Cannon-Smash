using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBall : Ball
{
    [SerializeField] private float GuidedTime;
    [SerializeField] private Vector3 CameraBallRelativePosition;
    [SerializeField] private PlayerMoveController ControllerScript;
    
    private static Vector3 cameraInitialPosition;
    private static Quaternion cameraInitialRotation;
    private static Camera cam;

    private float firedTimestamp;

    void Start()
    {
        firedTimestamp = 0;

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
                firedTimestamp = 0;

                // Go back to normal mode
                ControllerScript.leftController = null;
                TopRightPanel.Instance.gameObject.SetActive(true);
                Joystick.Instance.gameObject.SetActive(false);
                cam.transform.position = cameraInitialPosition;
            }
        }
    }

    override public void Fired()
    {
        firedTimestamp = Time.time;
        TopRightPanel.Instance.gameObject.SetActive(false);
        Joystick.Instance.gameObject.SetActive(true);
        ControllerScript.leftController = Joystick.Instance.Stick;
        cameraInitialPosition = cam.transform.position;
        cameraInitialRotation = cam.transform.rotation;
    }
}
