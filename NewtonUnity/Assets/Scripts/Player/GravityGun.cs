using System.Collections;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 100)] public float gravityPercent = 100f;
    public float scrollSpeed = 10f;

    [Header("References")]
    public Camera playerCamera;
    public Rigidbody playerRb;
    public HazardManager HazardManager;

    public GameObject hazardOne;
    public GameObject hazardTwo;

    public LineRenderer lineRenderer;
    public Transform muzzlePoint;
    public float beamDuration = 0.1f;
    public Color beamColor = Color.cyan;

    // Point light for the beam
    public Light beamLight;
    public float lightIntensity = 5f;
    public float lightRange = 10f;
    public Animator gunAnim;

    private void Start()
    {
        beamLight.enabled = false;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
            gravityPercent = Mathf.Clamp(gravityPercent + scroll * scrollSpeed, 0f, 100f);

        if (Input.GetMouseButtonDown(0))
            ShootObject();

        if (Input.GetMouseButtonDown(1))
        {
            gunAnim.SetTrigger("SelfPoint");
            ShootSelf();
        }

    }

    private IEnumerator BeamRoutine(Vector3 hitPoint)
    {
        lineRenderer.enabled = true;
        lineRenderer.startColor = beamColor;
        lineRenderer.endColor = (gravityPercent != 100) ? Color.red : Color.blue;

        // Enable the beam light
        if (beamLight != null)
        {
            beamLight.enabled = true;
            beamLight.intensity = lightIntensity;
            beamLight.range = lightRange;
            beamLight.transform.position = muzzlePoint.position;
        }

        float timer = 0f;
        float travelTime = beamDuration;

        while (timer < travelTime)
        {
            lineRenderer.SetPosition(0, muzzlePoint.position);

            float t = timer / travelTime;
            Vector3 currentEnd = Vector3.Lerp(muzzlePoint.position, hitPoint, t);
            lineRenderer.SetPosition(1, currentEnd);

            // Move the light along with the beam end
            if (beamLight != null)
                beamLight.transform.position = currentEnd;

            timer += Time.deltaTime;
            yield return null;
        }

        lineRenderer.SetPosition(1, hitPoint);

        yield return new WaitForSeconds(0.05f);

        lineRenderer.enabled = false;
        if (beamLight != null)
            beamLight.enabled = false;
    }

    void ShootObject()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            StartCoroutine(BeamRoutine(hit.point));

            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply a small force in the direction of the beam
                Vector3 forceDir = (hit.point - muzzlePoint.position).normalized;
                float forceAmount = 5f; // tweak this for desired push
                rb.AddForce(forceDir * forceAmount, ForceMode.Impulse);
            }

            if (rb != null && hit.collider.CompareTag("Objects"))
            {
                ApplyGravity(rb, gravityPercent);
            }
            else if (rb != null && hit.collider.CompareTag("HazardB1"))
            {
                ApplyGravity(rb, gravityPercent);
                if (gravityPercent != 0)
                {
                    HazardManager.hazards -= 1;
                    Destroy(hazardOne);
                }
            }
            else if (rb != null && hit.collider.CompareTag("HazardB2"))
            {
                ApplyGravity(rb, gravityPercent);
                if (gravityPercent != 0)
                {
                    HazardManager.hazards -= 1;
                    Destroy(hazardTwo);
                }
            }
        }
    }


    void ShootSelf()
    {
        if (playerRb != null)
            ApplyGravity(playerRb, gravityPercent);
    }

    public void ApplyGravity(Rigidbody rb, float gp)
    {
        rb.useGravity = false;
        float multiplier = gp / 100f;

        GravityController controller = rb.GetComponent<GravityController>();
        if (controller != null)
            Destroy(controller);

        controller = rb.gameObject.AddComponent<GravityController>();
        controller.multiplier = multiplier;
        Debug.Log(rb + " gravity set to " + multiplier);
    }
}
