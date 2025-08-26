using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHasWon : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.name == "Body")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
