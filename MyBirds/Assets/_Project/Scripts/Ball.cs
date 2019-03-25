using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public float worldYBoundary = -6f;
    public float maxStretch = 3.0f;
    public float epsilon;
    
    private SpringJoint spring;
    private Rigidbody rb;
    private SphereCollider sp;
    private AudioSource stretchSound;
    private AudioSource releaseSound;
    
    private MyCamera camScript;
    private Camera cam;
    private LineRenderer backLine;
    private LineRenderer frontLine;
    private bool clickedOn;
    private float maxStretchSqr;
    private Ray mouseRay;
    private Ray elasticRay;
    private Vector3 beforeDragPos;  // screen position of object before dragging
    private float dragOffsetX;  // how off center of the object we clicked
    private float dragOffsetY;
    private Vector3 prevVelocity;
    private LinkedList<GameObject> obstacleList;
    private float oldAngularDrag;
    private LinkedList<GameObject> monsterList;

    public delegate void BallThrownEventHandler(object source, EventArgs args);
    public event BallThrownEventHandler BallThrown;
    protected virtual void OnBallThrown()
    {
        if (BallThrown != null)
        {
            BallThrown(this, EventArgs.Empty);
        }
    }

    public delegate void BallOutEventHandler(object source, EventArgs args);
    public event BallOutEventHandler BallOut;
    protected virtual void OnBallOut()
    {
        if (BallOut != null)
        {
            BallOut(References.Group_Obstacles, EventArgs.Empty);
        }
    }

    public delegate void AllOutEventHandler(object source, EventArgs args);
    public event AllOutEventHandler AllOut;
    protected virtual void OnAllOut()
    {
        if (AllOut != null)
        {
            AllOut(this, EventArgs.Empty);
        }
    }

    void Start ()
    {
        spring = GetComponent<SpringJoint>();
        rb = GetComponent<Rigidbody>();
        sp = GetComponent<SphereCollider>();
        stretchSound = GetComponents<AudioSource>()[0];
        releaseSound = GetComponents<AudioSource>()[1];
        
        camScript = References.Main_Camera.GetComponent<MyCamera>();
        cam = References.Main_Camera.GetComponent<Camera>();
        frontLine = References.Catapult_FrontArm.GetComponent<LineRenderer>();
        backLine = References.Catapult_BackArm.GetComponent<LineRenderer>();
        clickedOn = false;
        maxStretchSqr = maxStretch * maxStretch;
        mouseRay = new Ray(spring.connectedBody.position, Vector3.zero);
        elasticRay = new Ray(frontLine.transform.position, Vector3.zero);
        oldAngularDrag = rb.angularDrag;
        obstacleList = Functions.GetChildrenOf(References.Group_Obstacles);
        monsterList = Functions.GetChildrenOf(References.Group_Monsters);

        LineSetup();
    }
	
	void FixedUpdate ()
    {
        if (spring != null)
        {
            if (!rb.isKinematic && prevVelocity.sqrMagnitude > rb.velocity.sqrMagnitude)
            {
                Destroy(spring);
                rb.velocity = prevVelocity;
                OnBallThrown();
            }

            if (clickedOn)
            {
                Drag();
            }
            else
            {
                prevVelocity = rb.velocity;
            }

            LineUpdate();
        }
        else
        {
            if (IsOutOfPlay(gameObject))
            {
                OnBallOut();
                if (AllOutOfPlay(obstacleList))
                {
                    OnAllOut();
                }
            }

            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }        

        // updating the text
        if (References.Text_TopLeftPanel.activeSelf)
        {
            References.Text_TopLeftPanel.GetComponent<TMPro.TextMeshProUGUI>().text = "" + Mathf.Round(rb.velocity.sqrMagnitude * 100) / 100;
        }
    }

    private void OnMouseDown()
    {
        clickedOn = true;
        beforeDragPos = cam.WorldToScreenPoint(transform.position);
        dragOffsetX = Input.mousePosition.x - beforeDragPos.x;
        dragOffsetY = Input.mousePosition.y - beforeDragPos.y;
        stretchSound.Play();
    }

    private void OnMouseUp()
    {
        clickedOn = false;
        rb.isKinematic = false;
        stretchSound.Stop();
        releaseSound.Play();
    }

    private void Drag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x - dragOffsetX, Input.mousePosition.y - dragOffsetY, beforeDragPos.z);
        Vector2 worldPos = cam.ScreenToWorldPoint(newPosition);
        Vector2 catapultToNewPos = worldPos - (Vector2)spring.connectedBody.position;

        if (catapultToNewPos.sqrMagnitude > maxStretchSqr)
        {
            mouseRay.direction = catapultToNewPos;
            worldPos = mouseRay.GetPoint(maxStretch);
        }

        transform.position = worldPos;
    }

    private void LineSetup()
    {
        frontLine.SetPosition(0, frontLine.transform.position);
        backLine.SetPosition(0, backLine.transform.position);
    }

    private void LineUpdate()
    {
        Vector3 frontArmToBall = transform.position - frontLine.transform.position;
        Vector3 backArmToBall = transform.position - backLine.transform.position;
        Vector3 attach;

        // draw frontLine elastic
        elasticRay.origin = frontLine.transform.position;
        elasticRay.direction = frontArmToBall;
        attach = elasticRay.GetPoint(frontArmToBall.magnitude);
        attach.z -= sp.radius;
        frontLine.SetPosition(1, attach);

        // draw backLine elastic
        elasticRay.origin = backLine.transform.position;
        elasticRay.direction = backArmToBall;
        attach = elasticRay.GetPoint(backArmToBall.magnitude);
        attach.z += sp.radius;
        backLine.SetPosition(1, attach);
    }

    private bool AllOutOfPlay(LinkedList<GameObject> list)
    {
        foreach (GameObject cur in list)
        {
            if (!IsOutOfPlay(cur))
            {
                return false;
            }
        }

        return true;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // GroundCheck
        if (collision.gameObject.name.Equals("Ground"))
        {
            rb.angularDrag = 15;
        }
        else
        {
            rb.angularDrag = oldAngularDrag;
        }
    }

    private bool IsOutOfPlay(GameObject obj)
    {
        if (obj.transform.position.y <= this.worldYBoundary)
        {
            return true;
        }
        else if (Functions.IsImmobile(obj, this.epsilon))
        {
            return true;
        }
        else
        {
            return !obj.activeInHierarchy;
        }
    }
}
