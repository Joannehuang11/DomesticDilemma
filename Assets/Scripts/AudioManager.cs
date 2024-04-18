using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject bgMusicPlayerObj;
    private AudioSource bgMusicPlayer;

    public GameObject clickSoundPlayerObj;
    private AudioSource clickSoundPlayer;

    public GameObject errorSoundPlayerObj;
    private AudioSource errorSoundPlayer;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playBgMusic()
    {
        bgMusicPlayer = bgMusicPlayerObj.GetComponent<AudioSource>();
        bgMusicPlayer.loop = true;
        bgMusicPlayer.Play();
    }

    public void playClickSound()
    {
        clickSoundPlayer = clickSoundPlayerObj.GetComponent<AudioSource>();
        clickSoundPlayer.Play();
    }

    public void playErrorSound()
    {
        errorSoundPlayer = errorSoundPlayerObj.GetComponent<AudioSource>();
        errorSoundPlayer.Play();
    }
}
