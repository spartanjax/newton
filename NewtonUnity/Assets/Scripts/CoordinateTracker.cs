using UnityEngine;

public class CoordinateTracker : MonoBehaviour
{
    public Transform target;  

    void Update()
    {
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = newPos;
        }
    }
}
