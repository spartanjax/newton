using UnityEngine;

public class RandomImpulse : MonoBehaviour
{
    public float forceStrength = 2f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomDir = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;

            rb.AddForce(randomDir * forceStrength, ForceMode.Impulse);
        }
    }
}
