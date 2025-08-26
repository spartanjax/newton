using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravKeyTips : MonoBehaviour
{
    public GameObject gravTip;
    public GameObject keyTip;

    public GravityManager GravityManager;
    public lockdownTrigger lockdownTrigger;
    private void Update()
    {
        if(GravityManager.hasDevice)
        {
            gravTip.SetActive(false);
            StartCoroutine(showKeyTip(10));
        }

        if(lockdownTrigger.tutorialDone){
            gravTip.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Body")
        {
            StartCoroutine(giveTips(10));
        }
    }

    IEnumerator giveTips(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gravTip.SetActive(true);
    }
    IEnumerator showKeyTip(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        keyTip.SetActive(true);
    }
}
