using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPush : MonoBehaviour
{
    public ResetManager resetManager;
    public Animator buttonAnim;
    public Animator lDoorAnim;
    public Animator rDoorAnim;

    public GameObject redLight;
    public GameObject greenLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Objects" && this.name == "RedButton")
        {
            // Start a coroutine to handle the delayed actions
            StartCoroutine(DelayedButtonPress());
        }
    }

    private IEnumerator DelayedButtonPress()
    {
        // Optional: play button press animation immediately
        buttonAnim.SetBool("isPressed", true);

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Execute actions after delay
        lDoorAnim.SetBool("isOpening", true);
        rDoorAnim.SetBool("isOpening", true);
        redLight.SetActive(false);
        greenLight.SetActive(true);
        resetManager.midLevel = true;
    }

}
