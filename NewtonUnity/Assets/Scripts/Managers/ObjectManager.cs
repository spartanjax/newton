using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public enum Objects {keyCard, coffeeMug, potPlant, penHolder, rVent, fVent, pushVent, GkeyCard, GravDevice, hubButton, glassShard1, miniPlatform, camButton, sparkPanel};
    public Objects whatAmI;

    //Object starting position and rotation
    private Vector3 startPos;
    private Quaternion startRot;
    private Rigidbody rb;

    //Stops the object from shrinking when interacted with
    public Vector3 size;
    private Vector3 scale;

    public bool reseting = false;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        //Finds where object starts in game, and saves that Vector3 and Quaternion
        startPos = this.transform.position;
        startRot = this.transform.rotation;

        //Gets size of object, so it can remain that size after being interacted with
        scale = this.transform.localScale;
    }

    private void Update()
    {
        //If the object glitches through map, replace it
        if (this.transform.position.y <= -10)
        {
            reset();
        }

        //If the size of the obejct ever changes, make it have its starting size
        if (this.transform.localScale != scale)
        {
            this.transform.localScale = scale;
        }

        if(reseting)
        {
            reset();
            reseting = false;
        }
    }

    private void reset()
    {
        //Send object back to starting position
        this.transform.position = startPos;
        this.transform.rotation = startRot;
        rb.velocity = Vector3.zero;
    }
}
