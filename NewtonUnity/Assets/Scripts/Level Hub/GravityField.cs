using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public GameObject blueScreen;
    public GravityController gravityController;
    public float waitTime;
    private bool isEntered;
    public GravityGun GravityGun;
    public Rigidbody playerRb;

    private void Update()
    {
        if(isEntered)
        {
            GravityGun.ApplyGravity(playerRb, 100);
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
        GravityGun.ApplyGravity(playerRb, 100);
        gravityController.multiplier = 100f;
    }
}
