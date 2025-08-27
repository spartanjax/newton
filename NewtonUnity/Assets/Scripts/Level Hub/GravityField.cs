using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public GameObject blueScreen;
    public GravityGun GravityGun;
    public float waitTime;
    private bool isEntered;
    public Rigidbody playerRb;
    public GameObject player;

    private void Update()
    {
        if(isEntered)
        {
            GravityGun.ApplyGravity(playerRb, 100, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.name == "Body")
        {
            blueScreen.SetActive(true);
            StartCoroutine(changeGrav());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        blueScreen.SetActive(false);
        isEntered = false;
    }
    IEnumerator changeGrav()
    {
        yield return new WaitForSeconds(waitTime);
        isEntered = true;

        GravityController g = player.GetComponent<GravityController>();
        if (g.multiplier != 100)
        {
            GravityGun.ApplyGravity(playerRb, 100, false);
        }
        //gravityController.multiplier = 100f;
    }
}
