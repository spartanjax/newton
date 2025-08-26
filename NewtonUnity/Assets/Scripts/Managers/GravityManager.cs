using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public float gravityAmount;
    private float gravityIncrement = 1f;
    public bool hasDevice = false;

    // Start is called before the first frame update
    void Start()
    {
        gravityAmount = -30; 
    }

    // Update is called once per frame
    void Update()
    {
        //Controls Gravity Amount in the Scene
        Physics.gravity = new Vector3(0, gravityAmount, 0);

        //Mouse scroll wheel input
        if (hasDevice && Input.GetAxis("Mouse ScrollWheel") > 0f && gravityAmount > -30)
        {
            gravityAmount -= gravityIncrement;
        }
        if (hasDevice && Input.GetAxis("Mouse ScrollWheel") < 0f && gravityAmount < 0)
        {
            gravityAmount += gravityIncrement;
        }

        Boundaries();
    }
    //Stops gravity from going outside of desired range
    private void Boundaries()
    {
        if (gravityAmount > 0)
        {
            gravityAmount = 0;
        }
        if (gravityAmount < -30)
        {
            gravityAmount = -30;
        }
    }
}
