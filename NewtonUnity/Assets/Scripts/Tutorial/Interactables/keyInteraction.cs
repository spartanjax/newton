using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyInteraction : MonoBehaviour
{
    //Object starting position and rotation
    private Vector3 startPos;
    private Quaternion startRot;

    //Tells object where to go when picked up
    public GameObject player;
    public Transform dest;
    private Rigidbody rb;

    //Stops the object from shrinking when interacted with
    public Vector3 size;
    private MeshRenderer renderery;

    //Keycard Animation
    public Animator keyAnim;
    public GameObject keyMech;
    public bool inserted;

    //Referenced Scripts
    public playerManager playerManager;
    public PlayerRaycast playerRaycast;
    public TutorialMananger tutorialMananger;

    private bool carryingObj = false;
    void Start()
    {
        //Setup
        rb = this.GetComponent<Rigidbody>();
        inserted = false;
        keyAnim.enabled = false;

        //Finds where object starts in game, and saves that Vector3 and Quaternion
        startPos = this.transform.position;
        startRot = this.transform.rotation;

        //Gets size of object, so it can remain that size after being interacted with
        renderery = GetComponent<MeshRenderer>();
        size = renderery.bounds.size;
    }

    void Update()
    {
        //If player clicks/holds mouse1, pickup or drop the object
        if (Input.GetMouseButton(0) && carryingObj)
        {
            drop();
            StartCoroutine(nocarry(0.2f));
        }
        else if (inserted == false && playerRaycast.isKeyObj && Vector3.Distance(player.transform.position, this.transform.position) < playerManager.pickupDist)
        {
            if (Input.GetMouseButton(0) && playerManager.isCarrying == false)
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "DoorTrigger")
        {
            //When the keycard nears a keyholder, it will reset and move to holder for animation
            inserted = true;
            tutorialMananger.keyEntered = true;
            this.transform.parent = null;
            this.transform.position = keyMech.transform.position;
            this.transform.rotation = new Quaternion(0, 0, 0, 0);

            //Enables the animation and keycard will be inserted
            keyAnim.enabled = true;
        }
    }
}
