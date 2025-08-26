using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardInteraction : MonoBehaviour
{
    //Tells object where to go when picked up
    public GameObject player;
    public Transform dest;
    private Rigidbody rb;
    public GameObject mainCamera;
    public GameObject throwTip;

    //Stops the object from shrinking when interacted with
    public Vector3 size;
    private MeshRenderer renderery;

    //Referenced Scripts
    public PlayerRaycast playerRaycast;
    public playerManager playerManager;

    private bool carryingObj;
    public bool hasThrown = false;

    void Start()
    {
        //Setup
        rb = this.GetComponent<Rigidbody>();

        //Gets size of object, so it can remain that size after being interacted with
        renderery = GetComponent<MeshRenderer>();
        size = renderery.bounds.size;
    }

    void Update()
    {
        //If player is carrying an object
        if (carryingObj)
        {
            if (Input.GetKeyDown(KeyCode.E)) // changed from Mouse0
            {
                drop();
                StartCoroutine(nocarry(0.2f));
            }
            if (Input.GetKeyDown(KeyCode.Q)) // changed from Mouse1
            {
                Destroy(throwTip);
                throwing();
                StartCoroutine(nocarry(0.2f));
            }
        }
        else if (playerRaycast.isShardOne && Vector3.Distance(player.transform.position, this.transform.position) < playerManager.pickupDist)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerManager.isCarrying == false) // changed from Mouse0
            {
                pickup();
                StartCoroutine(carry(0.2f));
            }
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

    private void throwing()
    {
        hasThrown = true;
        this.transform.parent = null;
        this.transform.position = GameObject.Find("throwLocation").transform.position;
        playerManager.isCarrying = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(mainCamera.transform.forward * Time.deltaTime * playerManager.throwStrength);
        rb.AddForce(player.transform.up * Time.deltaTime * playerManager.throwStrength / 3);
    }

    private void pickup()
    {
        playerManager.isCarrying = true;

        //When objects are picked up, they float and are parented under right hand.
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
