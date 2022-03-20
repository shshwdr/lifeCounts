using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{

    public AudioClip levelMusic;
    public AudioClip levelMusic2;
    public AudioClip homeMusic;
    public AudioClip urgentMusic;
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
        audioSource = GetComponent<AudioSource>();
        startPlay(homeMusic);
    }
    public void playUrgentMusic()
    {
        audioSource = GetComponent<AudioSource>();
        startPlay(urgentMusic);
    }
    public void playLevelMusic()
    {
        audioSource = GetComponent<AudioSource>();
        startPlay(levelMusic);
    }
    public void playLevelMusic2()
    {
        audioSource = GetComponent<AudioSource>();
        startPlay(levelMusic2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
