using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;

    private Plane positionPlane = new Plane();

    private Vector3 aimVector;

    public Camera gameCamera;

    public Shooter shooter;

    public float clickTimer = 0f;

    public int pickupOrder = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        GetComponentInChildren<Shooter>().BulletType = Shooter.BulletTypes.Peashot;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetButtonUp("Fire1") && clickTimer < 0.2f)
        {
            shooter.Shoot();
        }

        if (Input.GetButton("Fire1"))
        {
            clickTimer += Time.deltaTime;
        }
        else
        {
            clickTimer = 0;
        }
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
        
        // Hover
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, 1f, 1 << 9)) // 9 is the ground layer.
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

    public void Pickup(PickupType type, bool addOrder)
    {
        if (type == PickupType.fuel)
        {
            if (addOrder)
            {
                pickupOrder++;
            }
            FindObjectOfType<DestructionLevel>().Pickup(5, transform.position);
        }
    }

    public enum PickupType
    {
        fuel
    }
}
