using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private TextMeshProUGUI audioText;
    private bool isAudioOn = true;

    // Start is called before the first frame update
    void Start()
    {
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
        isAudioOn = !isAudioOn;
        backgroundMusic.mute = !isAudioOn;
        audioText.text = isAudioOn ? "Music On" : "Music Off";
    }
}
