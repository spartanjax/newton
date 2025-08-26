using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDisplay : MonoBehaviour
{
    public GameObject sparks;
    public PlayerRaycast playerRaycast;
    public bool lookingAt = false;
    public GameObject panelTip;
    public bool called = false;
    public GameObject hazardSign;
    public HazardManager hazardManager;

    // Update is called once per frame
    void Update()
    {
        if (!called && playerRaycast.isPanelButton)
        {
            panelTip.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(sparks);
                Destroy(panelTip);
                Destroy(hazardSign);
                called = true;
                hazardManager.hazards -= 1;
            }
        }
        else
        {
            if (!called)
            {
                panelTip.SetActive(false);
            }
        }

    }
}
