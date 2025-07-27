using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Image audioButtonImage;
    [SerializeField] private Sprite unmuteSprite;
    [SerializeField] private Sprite muteSprite;
    private bool isMuted = false;

    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;
    private bool isPaused = false;



    // Start is called before the first frame update
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("isMuted", 0) == 1;
        UpdateAudioState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleAudio();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void ToggleAudio()
    {
        isMuted = !isMuted;
        UpdateAudioState();
    }

    public void OnDestroy() {
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateAudioState()
    {
        backgroundMusic.mute = isMuted;
        audioButtonImage.sprite = isMuted ? muteSprite : unmuteSprite;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        buttonImage.sprite = isPaused ? playSprite : pauseSprite;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }  
}
