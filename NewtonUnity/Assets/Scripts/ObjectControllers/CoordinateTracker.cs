using UnityEngine;

public class CoordinateTracker : MonoBehaviour
{
    public Transform target;
    public bool trackingRotation;

    void Update()
    {
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = newPos;

            if (trackingRotation)
            {
                Vector3 currentRot = transform.eulerAngles;
                transform.rotation = Quaternion.Euler(currentRot.x, target.eulerAngles.y, currentRot.z);
            }
        }


    }
}
