using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    //Regular Door Animation
    public Animator doorAnimL;
    public Animator doorAnimR;

    //Bedroom door animation
    public Animator bedDoorAnim;
    public Animator bedDoorAnim2;

    //Referenced SCripts
    public lockdownTrigger lockdownTrigger;

    private void Update()
    {
        // Only run if lockdownTrigger is assigned
        if (lockdownTrigger != null && lockdownTrigger.hasTriggered)
        {
            if (doorAnimL != null) doorAnimL.SetBool("isOpening", false);
            if (doorAnimR != null) doorAnimR.SetBool("isOpening", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If keycard is inserted, the door will open
        if (this.name == "DoorTrigger" && other.name == "KeyCard")
        {
            doorAnimL.SetBool("isOpening", true);
            doorAnimR.SetBool("isOpening", true);
        }

        if (this.name == "GDoorTrigger" && other.name == "GreenKeycard")
        {

            doorAnimL.SetBool("isOpening", true);
            doorAnimR.SetBool("isOpening", true);
            lockdownTrigger.tutorialDone = true;
        }

        //If player stands near the door, it will open
        if (this.name == "bedroomDoorMechanism" && other.name == "Body")
        {
            bedDoorAnim.SetBool("isOpening", true);
        }
        if (this.name == "bedroomDoorMechanism2" && other.name == "Body")
        {
            bedDoorAnim2.SetBool("isOpening", true);
        }

        if (this.name == "LevelDoorMechanism" && other.name == "Body")
        {
            doorAnimL.SetBool("isOpening", true);
            doorAnimR.SetBool("isOpening", true);
        }
    }

    //If player or object leaves the certain trigger area, door will close
    private void OnTriggerExit(Collider other)
    {
        if (this.name == "bedroomDoorMechanism" && other.name == "Body")
        {
            bedDoorAnim.SetBool("isOpening", false);
        }
        if (this.name == "bedroomDoorMechanism2" && other.name == "Body")
        {
            bedDoorAnim2.SetBool("isOpening", false);
        }
        if (this.name == "LevelDoorMechanism" && other.name == "Body")
        {
            doorAnimL.SetBool("isOpening", false);
            doorAnimR.SetBool("isOpening", false);
        }
    }
}
