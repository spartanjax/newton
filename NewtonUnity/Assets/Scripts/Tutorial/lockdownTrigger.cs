using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockdownTrigger : MonoBehaviour
{

    public GameObject invisibleWall;

    //Controlled variables that change according to circumstance
    private float waitTime;
    private float shakeTime;
    private float speedChange;
    public bool tutorialDone = false;
    public GameObject crouchTip;

    //Animators
    public Animator lDoorAnim;
    public Animator rDoorAnim;
    public Animator sDoorAnim;
    public Animator s2DoorAnim;

    public bool hasTriggered = false;

    //Referenced Scripts
    public cameraShake cameraShake;
    public PlayerMovement playerMovement;

    private void Update()
    {
        float randomTime = Random.Range(1,3);

        if(hasTriggered && Input.GetKey(KeyCode.LeftShift))
        {
            crouchTip.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Body" && hasTriggered == false)
        {
            //Stops player from going back through
            invisibleWall.SetActive(true);
            hasTriggered = true;
            //Shakes the players camera
            shakeTime = 1.5f;
            cameraShake.waitShake = 0;
            StartCoroutine(cameraShake.Shake(shakeTime, 0.15f));
            StartCoroutine(playerSpeed(0.2f));

            //Close door in front of player
            //takes 2 more seconds for doors to shut completely
            StartCoroutine(doorClose(1f));

            StartCoroutine(showCrouchTip(7f));


            //Player cannot reactivate the event
            StartCoroutine(destroyTrigger(3f));

        }
    }
    IEnumerator doorClose(float waitTime)
    {
        //close front door
        yield return new WaitForSeconds(waitTime);
        lDoorAnim.SetBool("isClosing", true);
        rDoorAnim.SetBool("isClosing", true);
        sDoorAnim.SetBool("isLockdown", true);

        //close back door
        s2DoorAnim.SetBool("isLockdown", true);

    }
    IEnumerator destroyTrigger(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this);
    }

    IEnumerator showCrouchTip(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        crouchTip.SetActive(true);
    }

    IEnumerator playerSpeed(float waitTime)
    {
        //Slow down the character, increases immersivity of shaking etc... 
        //Also stops the player from getting to close to the far door as to see the stairs lead nowhere
        yield return new WaitForSeconds(waitTime);
        speedChange = 2.5f;
        playerMovement.moveSpeed = playerMovement.moveSpeed / speedChange;
        playerMovement.maxSpeed = playerMovement.maxSpeed / speedChange;

        waitTime = shakeTime;

        //Reset player speed to original speed
        yield return new WaitForSeconds(waitTime);
        playerMovement.moveSpeed = playerMovement.startMoveSpeed;
        playerMovement.maxSpeed = playerMovement.startMaxSpeed;
        yield return new WaitForSeconds(1f);
        if (playerMovement.moveSpeed != playerMovement.startMoveSpeed)
        {
            playerMovement.moveSpeed = playerMovement.startMoveSpeed;
            playerMovement.maxSpeed = playerMovement.startMaxSpeed;
        }
    }
}