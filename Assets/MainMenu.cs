using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Launching game !");
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
