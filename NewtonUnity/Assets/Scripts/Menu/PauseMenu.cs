using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    public GameObject menu;
    public GameObject playerHUD;
    public PlayerMovement playerMovement;
    

    public void OnResume()
    {
        Time.timeScale = 1;
        StartCoroutine(unpausing());
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            StartCoroutine(pausing());
        }
    }

    IEnumerator pausing()
    {

        playerHUD.SetActive(false);

        playerMovement.enabled = false;
        Time.timeScale = 0;

        menu.SetActive(true);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        yield return new WaitForSeconds(0.3f);

    }
    IEnumerator unpausing()
    {
        playerMovement.enabled = true;
        Time.timeScale = 1;

        menu.SetActive(false);
        playerHUD.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yield return new WaitForSeconds(0.3f);
       
    }
}
