using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midLevel : MonoBehaviour
{
    public ResetManager resetManager;
    private void OnTriggerEnter(Collider other)
    {
        if(this.name == "LevelDoorMechanism" && other.name == "Body")
        {
            resetManager.midLevel = true;
        }
    }
}
