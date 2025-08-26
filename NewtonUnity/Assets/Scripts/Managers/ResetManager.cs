using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    private int currentCheckpoint;
    public bool midLevel;
    public GameObject player;
    
    public GameObject head;
    public ObjectManager shardManager1;
    public ObjectManager shardManager2;
    public ObjectManager shardManager3;
    public ObjectManager shardManager4;

    //Checkpoints
    public GameObject levelOne;
    public GameObject levelTwo;

    void Start()
    {
        currentCheckpoint = 0;
        midLevel = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R) && currentCheckpoint == 1 && !midLevel)
        {
            player.transform.position = levelOne.transform.position; 
            head.transform.rotation = new Quaternion(0, 90, 0, 0);
            shardManager1.reseting = true;
            shardManager2.reseting = true;
            shardManager3.reseting = true;
            shardManager4.reseting = true;
        }

        if(Input.GetKey(KeyCode.R) && currentCheckpoint == 2 && !midLevel)
        {
            player.transform.position = levelTwo.transform.position; 
            player.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if(other.name == "checkpointOneTrigger")
        {
            currentCheckpoint = 1;
            midLevel = false;
        }

        if(other.name == "checkpointTwoTrigger")
        {
            currentCheckpoint = 2;
            midLevel = false;
        }
    }

    
}
