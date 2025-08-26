using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    public int hazards = 3;
    public GameObject NeutralizedMessage;
    public UnlockdownButton unlockdownButton;
    public GameObject buttonLight;
    public GameObject computerMessage;

    void Update()
    {
        if (hazards == 0)
        {
            NeutralizedMessage.SetActive(true);
            unlockdownButton.hazards = false;
            computerMessage.SetActive(true);

            if (unlockdownButton.called == false) {
                buttonLight.SetActive(true);
            }
        }
    }
}
