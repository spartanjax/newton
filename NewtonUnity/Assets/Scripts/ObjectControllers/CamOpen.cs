using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOpen : MonoBehaviour
{
    public GameObject intercom;
    public Animator blastDoorAnim;

    public PlayerRaycast playerRaycast;
    public bool lookingAt = false;
    public GameObject buttonTip;
    public bool called = false;

    // Update is called once per frame
    void Update()
    {
        if (!called && playerRaycast.isCamButton)
        {
            buttonTip.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                blastDoorAnim.SetBool("buttonPressed", true);
                Destroy(buttonTip);
                called = true;
            }
        }
        else
        {
            if (!called)
            {
                buttonTip.SetActive(false);
            }
        }

    }
}
