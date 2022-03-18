using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{

    public AudioClip levelMusic;
    public AudioClip homeMusic;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void startPlay(AudioClip select)
    {
        if (audioSource.clip != select)
        {

            audioSource.clip = select;
            audioSource.Play();
        }
    }
    public void playHomeMusic()
    {
        startPlay(homeMusic);
    }
    public void playLevelMusic()
    {
        startPlay(levelMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
