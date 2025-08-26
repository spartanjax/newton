using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Trigger : MonoBehaviour
{
    public GameObject text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Body" && this.name == "checkpointOneTrigger")
        {
            StartCoroutine(throwTip());
        }
    }

    IEnumerator throwTip()
    {
        yield return new WaitForSeconds(5);
        text.SetActive(true);
    }
}
