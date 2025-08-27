using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public Transform holdPoint;
    public Transform throwLocation;

    [Header("Settings")]
    public float rayDistance = 5f;
    public float throwForce = 10f;

    private GameObject heldObject;
    private Rigidbody heldRb;
    private Collider heldCollider;
    public Collider playerCollider;

    void Update()
    {
        // Try pickup
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
                TryPickup();
            else
                Drop();
        }

        // Throw if holding
        if (heldObject != null && Input.GetKeyDown(KeyCode.Q))
        {
            Throw();
        }

        // Keep object at hold point if carrying
        if (heldObject != null)
        {
            heldObject.transform.position = holdPoint.position;
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Objects"))
            {
                heldObject = hit.collider.gameObject;
                heldRb = heldObject.GetComponent<Rigidbody>();
                heldCollider = heldObject.GetComponent<Collider>();

                if (heldRb != null)
                {
                    heldRb.useGravity = false;
                    //heldRb.constraints = RigidbodyConstraints.FreezeAll;
                    heldRb.constraints = RigidbodyConstraints.FreezeRotation; 
                    heldRb.velocity = Vector3.zero;
                    Physics.IgnoreCollision(heldCollider, playerCollider, true);
                }

                // Parent to player hold point
                heldObject.transform.position = holdPoint.position;
                heldObject.transform.parent = holdPoint;
            }
        }
    }

    void Drop()
    {
        if (heldObject != null)
        {
            heldObject.transform.parent = null;

            if (heldRb != null)
            {
                heldRb.useGravity = true;
                heldRb.constraints = RigidbodyConstraints.None;
                Physics.IgnoreCollision(heldCollider, playerCollider, false);
            }

            heldObject = null;
            heldRb = null;
        }
    }

    void Throw()
    {
        if (heldObject != null)
        {
            heldObject.transform.parent = null;

            if (heldRb != null)
            {
                heldRb.constraints = RigidbodyConstraints.None;
                heldRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

                // Use throwLocation position and forward for velocity
                Vector3 throwDir = throwLocation != null ? throwLocation.forward : cam.transform.forward;
                heldObject.transform.position = throwLocation != null ? throwLocation.position : holdPoint.position;
                heldRb.velocity = throwDir * throwForce;
                Physics.IgnoreCollision(heldCollider, playerCollider, false);
            }

            heldObject = null;
            heldRb = null;
        }
    }
}
