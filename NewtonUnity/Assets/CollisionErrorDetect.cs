using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionErrorDetect : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name);
    }
}
