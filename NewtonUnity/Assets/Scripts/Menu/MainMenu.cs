using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Options;
    private Vector3 startPos;
    public float transitionSpeed;
    private bool settings;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        startPos = new Vector3(Options.transform.position.x, Screen.height*1.5f, Options.transform.position.z);
        Options.transform.position = startPos;
        Options.SetActive(false);
    }
    public void Update()
    {
        if (settings && Options.transform.position.y > Screen.height/2)
        {
            Options.SetActive(true);
            Options.transform.Translate(Vector3.down * transitionSpeed * Time.deltaTime, Space.World);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Soooon");
        Application.Quit();
    }

    public void OptionsMenu()
    {
        settings = true;
    }
    public void BackToMain()
    {
        settings = false;
        Options.SetActive(false);
        Options.transform.position = startPos;
    }
}
