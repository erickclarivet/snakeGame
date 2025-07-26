using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{  
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        buttonImage.sprite = isPaused ? playSprite : pauseSprite;
    }   
}
