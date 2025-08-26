using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float multiplier = 1f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.AddForce(Physics.gravity * multiplier, ForceMode.Acceleration);
        }
    }
}
