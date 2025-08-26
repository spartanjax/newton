using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Options;
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
        Menu.SetActive(false);
        Options.SetActive(true);
    }
    public void BackToMain()
    {
        Menu.SetActive(true);
        Options.SetActive(false);
    }
}
