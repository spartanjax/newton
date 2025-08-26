using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public bool isCarrying;

    //Length player can interact with objects from
    public float pickupDist;

    public GameObject currentCheckpoint;

    public float throwStrength;

    //Referenced Scripts
    public pushVentInteraction pushVentInteraction;

    private void Start()
    {
        isCarrying = false;
        this.transform.position = currentCheckpoint.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "push vent")
        {
            pushVentInteraction.canInteract = true;
        }
    }
}
