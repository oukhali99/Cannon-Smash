using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMoveController : MonoBehaviour {

	// PUBLIC
	public SimpleTouchController leftController;
	public SimpleTouchController rightController;
	public Transform headTrans;
	public float XVelocityChange = 5f;
	public float speedContinuousLook = 100f;
	public float speedProgressiveLook = 3000f;

	// PRIVATE
	private Rigidbody _rigidbody;
    private bool resetted;
    private bool slowMo;

	[SerializeField] bool continuousRightController = true;
    [SerializeField] private float XVelocitySlowdown;

    void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		if (rightController != null) rightController.TouchEvent += RightController_TouchEvent;
        resetted = false;
	}

	public bool ContinuousRightController
	{
		set{continuousRightController = value;}
	}

	void RightController_TouchEvent (Vector2 value)
	{
		if(!continuousRightController)
		{
			UpdateAim(value);
		}
	}

	void Update()
	{
        // move
        if (leftController != null)
        {
            if (!resetted)
            {
                leftController.GetTouchPosition = Vector2.zero;
                resetted = true;
            }
            else
            {
                Vector2 touchPosition = leftController.GetTouchPosition;

                if (touchPosition.x != 0)
                {
                    _rigidbody.velocity += Time.deltaTime * XVelocityChange * Vector3.right * touchPosition.x;
                }
                else
                {
                    Vector3 slowdown = XVelocitySlowdown * Vector3.right;
                    Vector3 velocity = _rigidbody.velocity;
                    
                    if (velocity.x > slowdown.x)
                    {
                        velocity -= slowdown * Time.deltaTime;
                    }
                    else
                    {
                        velocity -= Vector3.right * velocity.x * Time.deltaTime;
                    }
                }
            }
        }

        if (continuousRightController)
        {
            if (rightController != null) UpdateAim(rightController.GetTouchPosition);
        }
    }

	void UpdateAim(Vector2 value)
	{
		if(headTrans != null)
		{
			Quaternion rot = Quaternion.Euler(0f,
				transform.localEulerAngles.y - value.x * Time.deltaTime * -speedProgressiveLook,
				0f);

			_rigidbody.MoveRotation(rot);

			rot = Quaternion.Euler(headTrans.localEulerAngles.x - value.y * Time.deltaTime * speedProgressiveLook,
				0f,
				0f);
			headTrans.localRotation = rot;

		}
		else
		{

			Quaternion rot = Quaternion.Euler(transform.localEulerAngles.x - value.y * Time.deltaTime * speedProgressiveLook,
				transform.localEulerAngles.y + value.x * Time.deltaTime * speedProgressiveLook,
				0f);

			_rigidbody.MoveRotation(rot);
		}
	}

	void OnDestroy()
	{
        if (rightController != null) rightController.TouchEvent -= RightController_TouchEvent;
	}

}
