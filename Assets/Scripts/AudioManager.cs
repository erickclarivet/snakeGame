using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Image audioButtonImage;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite muteSprite;
    private bool isMuted = false;

    // Start is called before the first frame update
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("isMuted", 0) == 1;
        UpdateAudioState();
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode toggleAudioKey = KeyCode.M;
        if (Input.GetKeyDown(toggleAudioKey))
        {
            ToggleAudio();
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
        audioButtonImage.sprite = isMuted ? muteSprite : playSprite;
    }
}
