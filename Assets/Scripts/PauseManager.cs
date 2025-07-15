using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseManager : MonoBehaviour
{  
    [SerializeField] private TextMeshProUGUI pauseText;
    //[SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
       // pauseMenu.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode pauseKey = KeyCode.P; // Change this to your desired pause key
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        pauseText.text = isPaused ? "Resume" : "Pause";
        // Affiche/masque le panneau pause
       // pauseMenu.SetActive(isPaused);
    }   
}
