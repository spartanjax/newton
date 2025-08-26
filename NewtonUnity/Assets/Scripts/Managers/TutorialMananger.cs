using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMananger : MonoBehaviour
{
    //Overall control variable
    public bool notDone;

    //Variables so player spawns at chosen checkpoint
    public GameObject currentCheckpoint;
    public GameObject player;

    //Tip texts
    public GameObject movementTip;
    public GameObject jumpingTip;
    public GameObject interactionTip;
    public GameObject escapeTip;

    //Checkpoints
    public GameObject c1;
    public GameObject c2;
    public GameObject c3;

    //Disallows tips to occur more than once 
    public bool keyEntered = false;

    //situational booleans to ensure events occur
    public bool keyEnable = false;
    public keyInteraction keyInteraction;

    //Level getting cleared before Checkpoint3
    public GameObject ground;
    public GameObject walls;
    public GameObject office;
    public GameObject hallway;
    public GameObject vents;
    public GameObject intobjects;
    public GameObject extraGround;

    // Start is called before the first frame update
    void Start()
    {
        //Reset all text activation
        clearAll();
        notDone = true;

        //Spawns player at the appropriate Checkpoint
        player.transform.position = currentCheckpoint.transform.position;

        StartCoroutine(firstRoom());

    }

    // Update is called once per frame
    void Update()
    {
        //Clear all 'starting' tips if player leaves the first area
        if (keyEntered)
        {
            notDone = false;
            clearAll();
        }
        
        //Clears all tips if the player spawns past the first area (I.e. Checkpoint 2)
        if (player.transform.position.z >= c2.transform.position.z)
        {
            notDone = false;
            clearAll();
        }

        //Destroying prior areas in level for better input and less lag
        if (player.transform.position.y <= c3.transform.position.y) 
        {
            //clearCp3();
        }
    }
 
    IEnumerator firstRoom()
    {
        //Show first tip
        yield return new WaitForSeconds(3);
        if (notDone == true) movementTip.SetActive(true);
        yield return new WaitForSeconds(5);
        clearAll();

        //Show second tip
        yield return new WaitForSeconds(5);
        if (notDone == true) jumpingTip.SetActive(true);
        yield return new WaitForSeconds(5);
        clearAll();

        yield return new WaitForSeconds(5);
        if (notDone == true) interactionTip.SetActive(true);
        yield return new WaitForSeconds(10);
        clearAll();

        yield return new WaitForSeconds(5);
        if (notDone == true) escapeTip.SetActive(true);
    }

    private void clearAll()
    {
        movementTip.SetActive(false);
        jumpingTip.SetActive(false);
        interactionTip.SetActive(false);
        escapeTip.SetActive(false);
    }

    private void clearCp3()
    {
        Destroy(walls);
        Destroy(ground);
        Destroy(vents);
        Destroy(hallway);
        Destroy(office);
        Destroy(intobjects);
        Destroy(extraGround);
    }
}