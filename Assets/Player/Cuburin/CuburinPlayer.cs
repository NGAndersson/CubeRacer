using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuburinPlayer : BasePlayer
{
    private new Rigidbody rigidbody;

    private Plane positionPlane = new Plane();

    private Vector3 aimVector;

    private bool rotateClockwise = false;

    public float rotationSpeed = 200f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        Camera.main.GetComponent<CameraController>().moveLerpFactor = 1f;
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        Move();
    }

    private void Move()
    {
        rigidbody.AddForce(new Vector3(Input.GetAxis("Horizontal")*10, 0, 0), ForceMode.Acceleration);
        rigidbody.AddForce(new Vector3(0, 0, Input.GetAxis("Vertical")*10), ForceMode.Acceleration);
        if (Input.GetButton("Fire1") || Input.touches.Length > 0)
        {
            rigidbody.velocity = GetAimRotation() * 10 * (Mathf.Max(Input.touches.Length, 1)/2 + 1);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }

        bool onGround = Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, 1f, 1 << 9); // 9 is the ground layer.

        // Rotate
        transform.Rotate(0, rotationSpeed * Time.deltaTime * (rotateClockwise ? 1 : -1), 0);

        // Hover
        if (onGround) 
        {
            rigidbody.useGravity = false;
            positionPlane.SetNormalAndPosition(hitInfo.normal, transform.position);
            transform.position = Vector3.Lerp(transform.position, hitInfo.point + hitInfo.normal * 1, 0.1f);
        }
        else // Mid-air
        {
            rigidbody.useGravity = true;
            positionPlane.SetNormalAndPosition(new Vector3(0, 1, 0), transform.position);
        }
    }

    private Vector3 GetAimRotation()
    {
        Ray r;
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Debug.Log("Test");

            r = gameCamera.ScreenPointToRay(Input.GetTouch(0).position);
            positionPlane.Raycast(r, out float distanceToPoint);
            aimVector = r.GetPoint(distanceToPoint) - transform.position;
            return aimVector.normalized;
        }
        else if (Application.isEditor && Input.GetMouseButton(0)) // For testing mobile input in editor.
        {
            r = gameCamera.ScreenPointToRay(Input.mousePosition);
            positionPlane.Raycast(r, out float distanceToPoint);
            aimVector = r.GetPoint(distanceToPoint) - transform.position;
            return aimVector.normalized;
        }
        else
        {
            return Vector3.zero; // Used instead of null.
        }
#elif !UNITY_ANDROID
        r = gameCamera.ScreenPointToRay(Input.mousePosition);
        positionPlane.Raycast(r, out float distanceToPoint);
        aimVector = r.GetPoint(distanceToPoint) - transform.position;
        return aimVector.normalized;
#endif
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            FindObjectOfType<CuburinLevel>().RestartLevel();
        }
        else if (collision.gameObject.tag == "bounce")
        {
            rotateClockwise = !rotateClockwise;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "goal")
        {
            FindObjectOfType<CuburinLevel>().Goal();
        }
    }
}
