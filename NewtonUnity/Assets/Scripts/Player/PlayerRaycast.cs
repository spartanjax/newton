using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    //Distance player can interact at
    public float distanceToSee;
    public GameObject cameraParent;

    //Store what object the player is looking at
    RaycastHit whatIHit;

    //Referenced scripts
    public gravDeviceManager gravDeviceManager;
    public UnlockdownButton unlockdownButton;

    //Interactable Objects

    //Tutorial
    public bool isKeyObj;
    public bool isCofObj;
    public bool isPotObj;
    public bool isPenObj;
    public bool isrVentObj;
    public bool isfVentObj;
    public bool isPushVentObj;
    public bool isGKeyObj;

    //Level Hub
    public bool isHubButtonObj;
    public bool isCamButton;
    public bool isPanelButton;

    //Level 1
    public bool isShardOne;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(cameraParent.transform.position, cameraParent.transform.forward * distanceToSee, Color.magenta);

        if(Physics.Raycast(cameraParent.transform.position, cameraParent.transform.forward, out whatIHit, distanceToSee))
        {
            if (whatIHit.collider.tag == "Objects")  
                {
                //Tutorial Objects
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.keyCard)
                {
                    isKeyObj = true; 
                }
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.coffeeMug)
                {
                    isCofObj = true;
                }
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.potPlant)
                {
                    isPotObj = true;
                }
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.penHolder)
                {
                    isPenObj = true;
                }
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.rVent)
                {
                    isrVentObj = true;
                }
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.fVent)
                {
                    isfVentObj = true;
                }
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.pushVent)
                {
                    isPushVentObj = true;
                }
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.GkeyCard)
                {
                    isGKeyObj = true;
                }
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.hubButton)
                {
                    isHubButtonObj = true;
                }
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.sparkPanel)
                {
                    isPanelButton = true;
                }
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.camButton)
                {
                    isCamButton = true;
                }

                //Picking Up Gravity Device 
                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.GravDevice)
                {
                    gravDeviceManager.lookingAt = true;
                }


                if (whatIHit.collider.gameObject.GetComponent<ObjectManager>().whatAmI == ObjectManager.Objects.glassShard1)
                {
                    isShardOne = true;
                }

            }
            else
            {
                isKeyObj = false;
                isCofObj = false;
                isPotObj = false;   
                isPenObj = false;
                isfVentObj = false;
                isrVentObj = false;
                isPushVentObj = false;
                isGKeyObj = false;
                isHubButtonObj = false;
                isCamButton = false;
                unlockdownButton.lookingAt = false;
                isPanelButton = false;
                isShardOne = false;
            }
        }
    }
}
