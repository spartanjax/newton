using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rVentInteraction : MonoBehaviour
{
    //Tells object where to go when picked up
    public GameObject player;
    public Transform dest;
    private Rigidbody rb;
    private bool carryingObj;

    //Referenced Scripts
    public PlayerRaycast playerRaycast;
    public playerManager playerManager;
    public PlayerMovement playerMovement;


    void Start()
    {
        //Setup
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //If player clicks/holds mouse1, pickup or drop the object
        if (Input.GetMouseButton(0) && carryingObj)
        {
            drop();
            StartCoroutine(nocarry(0.2f));
        }
        else if (playerRaycast.isrVentObj && Vector3.Distance(player.transform.position, this.transform.position) < playerManager.pickupDist)
        {
            if (Input.GetMouseButton(0) && playerManager.isCarrying == false)
            {
                pickup();
                StartCoroutine(carry(0.2f));
            }
        }

        if (playerMovement.forceDrop)
        {
            drop();
            StartCoroutine(nocarry(0.2f));
            playerMovement.forceDrop = false;
        }
    }

    IEnumerator carry(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        carryingObj = true;
    }
    IEnumerator nocarry(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        carryingObj = false;
    }
    private void pickup()
    {
        playerManager.isCarrying = true;

        //When objects are picked up, they float and are parented under right hand.
        GetComponent<Rigidbody>().useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        this.transform.position = dest.position;
        this.transform.parent = GameObject.Find("RightHand").transform;
    }
    private void drop()
    {
        //When objects are dropped, they are unparented and no longer float. 
        this.transform.parent = null;
        rb.constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().useGravity = true;
        playerManager.isCarrying = false;
    }
}