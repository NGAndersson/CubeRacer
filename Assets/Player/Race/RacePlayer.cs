using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlayer : BasePlayer
{
    private new Rigidbody rigidbody;

    private Plane positionPlane = new Plane();

    private Vector3 aimVector;

    public int pickupOrder = 0;

    public TrailRenderer trail;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
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
            rigidbody.AddForce(transform.forward * 10, ForceMode.Acceleration);
        }


        // Get aim vector
        Vector3 aimVector = GetAimRotation();

        bool onGround = Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, 1f, 1 << 9); // 9 is the ground layer.

        // Hover
        if (onGround) 
        {
            rigidbody.useGravity = false;
            positionPlane.SetNormalAndPosition(hitInfo.normal, transform.position); // Try with hitInfo.normal later.
            transform.position = Vector3.Lerp(transform.position, hitInfo.point + hitInfo.normal * 1, 0.1f);

            if (aimVector != Vector3.zero) 
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimVector, hitInfo.normal), 1f);
        }
        else // Mid-air
        {
            rigidbody.useGravity = true;
            positionPlane.SetNormalAndPosition(new Vector3(0, 1, 0), transform.position); // Try with hitInfo.normal later.

            if (aimVector != Vector3.zero)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimVector, new Vector3(0, 1)), 0.1f);
        }

        if (Vector3.Angle(rigidbody.velocity, aimVector) > 90 && rigidbody.velocity.magnitude > 4 && onGround)
        {
            trail.emitting = true;
        }
        else
        {
            trail.emitting = false;
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
}
