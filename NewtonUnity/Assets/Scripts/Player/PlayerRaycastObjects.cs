using UnityEngine;

public class PlayerRaycastObjects : MonoBehaviour
{
    public float rayDistance;       // How far the player can interact
    public Camera cam;                   // Reference to main camera
    public GameObject lookedAtObject;    // Object currently looked at

    void Update()
    {
        lookedAtObject = null;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Objects"))
            {
                lookedAtObject = hit.collider.gameObject;
            }
        }
    }
}
