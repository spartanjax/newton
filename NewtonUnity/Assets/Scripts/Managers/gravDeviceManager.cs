using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravDeviceManager : MonoBehaviour
{
    public bool lookingAt = false;
    public GameObject pressE;
    public GameObject fakegravDev;
    public GameObject realgravDev;
    public GravityManager gravityManager;

    // Update is called once per frame
    void Update()
    {
        if (lookingAt == true)
        {
            pressE.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                fakegravDev.SetActive(false);
                realgravDev.SetActive(true);
                gravityManager.hasDevice = true;
                pressE.SetActive(false);
            }
        }

        if(lookingAt == false)
        {
            pressE.SetActive(false);
        }
    }
}
