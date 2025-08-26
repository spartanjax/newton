using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gravityDisplay : MonoBehaviour
{
    public GameObject gravText;
    public Slider gravityText;
    public GravityGun GravityGun;

    private float gravityInt;


    // Update is called once per frame
    void Update()
    {
        gravityInt = (GravityGun.gravityPercent)/100;

        if (gravText != null)
        {
            gravText.SetActive(true);
        }

        gravityText.value = gravityInt;
        
    }
}
