using System.Collections;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 100)] public float gravityPercent = 100f;
    public float scrollSpeed = 10f;
    public float shootCooldown = 0.2f;
    public float delayTime = 0.5f;

    [Header("References")]
    public Camera playerCamera;
    public Rigidbody playerRb;
    public HazardManager HazardManager;

    public GameObject hazardOne;
    public GameObject hazardTwo;
    private bool hazOne = false;
    private bool hazTwo = false;

    public LineRenderer lineRenderer;
    public Transform muzzlePoint;
    public float beamDuration = 0.1f;
    public Color beamColor = Color.cyan;

    [Header("Effects")]
    public Light beamLight;
    public float lightIntensity = 5f;
    public float lightRange = 10f;
    public Animator gunAnim;
    public GameObject sparkEffectPrefab;

    private float lastShootTime = 0f;

    private void Start()
    {
        if (beamLight != null) beamLight.enabled = false;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
            gravityPercent = Mathf.Clamp(gravityPercent + scroll * scrollSpeed, 0f, 100f);

        // Shoot object with cooldown
        if (Input.GetMouseButtonDown(0) && Time.time - lastShootTime >= shootCooldown)
        {
            gunAnim.SetTrigger("Shoot");
            ShootObject();
            lastShootTime = Time.time;
            //gunAnim.ResetTrigger("Shoot");
        }

        if (Input.GetMouseButtonDown(1) && Time.time - lastShootTime >= shootCooldown)
        {
            gunAnim.SetTrigger("SelfPoint");
            ShootSelf();
            lastShootTime = Time.time;
            //gunAnim.ResetTrigger("SelfPoint");
        }
    }

    private IEnumerator BeamRoutine(Vector3 hitPoint)
    {
        lineRenderer.enabled = true;
        lineRenderer.startColor = beamColor;
        lineRenderer.endColor = (gravityPercent != 100) ? Color.red : Color.blue;

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

            // Spawn spark effect facing from muzzle to hit point
            if (sparkEffectPrefab != null)
            {
                Vector3 direction = (hit.point - muzzlePoint.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);
                GameObject spark = Instantiate(sparkEffectPrefab, hit.point, rotation);
                Destroy(spark, 1f);
            }

            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            Debug.Log(rb.gameObject.name);

            //if hit an object, apply small force
            if (rb != null)
            {
                Vector3 forceDir = (hit.point - muzzlePoint.position).normalized;
                float forceAmount = 5f;
                rb.AddForce(forceDir * forceAmount, ForceMode.Impulse);
            }

            //hit an adjustable object
            if (rb != null && (hit.collider.CompareTag("Objects") || hit.collider.CompareTag("ObjectsL")))
            {
                ApplyGravity(rb, gravityPercent, true);
            }

            //special cases for hazard scene
            else if (rb != null && hit.collider.CompareTag("HazardB1") && !hazOne)
            {
                ApplyGravity(rb, gravityPercent, true);
                if (gravityPercent != 0)
                {
                    HazardManager.hazards -= 1;
                    Destroy(hazardOne);
                    hazOne = true;
                }
            }
            else if (rb != null && hit.collider.CompareTag("HazardB2") && !hazTwo)
            {
                ApplyGravity(rb, gravityPercent, true);
                if (gravityPercent != 0)
                {
                    HazardManager.hazards -= 1;
                    Destroy(hazardTwo);
                    hazTwo = true;
                }
            }
        }
    }

    void ShootSelf()
    {
        if (playerRb != null)
            ApplyGravity(playerRb, gravityPercent, true);
    }

    public void ApplyGravity(Rigidbody rb, float gp, bool delay)
    {
        if (delay)
        {
            StartCoroutine(ApplyGravityWithDelay(rb, gp));
        } else
        {
            rb.useGravity = false;
            float multiplier = gp / 100f;

            //Get rid of all grav controllers
            GravityController[] controllers = rb.GetComponents<GravityController>();
            foreach (var c in controllers)
            {
                Destroy(c);
            }

            //Add another grav controller
            GravityController newController;
            newController = rb.gameObject.AddComponent<GravityController>();
            newController.multiplier = multiplier;
        }
    }

    //ApplyGravity function moved into aa delayed Coroutine
    private IEnumerator ApplyGravityWithDelay(Rigidbody rb, float gp)
    {
        yield return new WaitForSeconds(delayTime);

        rb.useGravity = false;
        float multiplier = gp / 100f;

        GravityController controller = rb.GetComponent<GravityController>();
        if (controller != null)
            Destroy(controller);

        controller = rb.gameObject.AddComponent<GravityController>();
        controller.multiplier = multiplier;
    }

}
