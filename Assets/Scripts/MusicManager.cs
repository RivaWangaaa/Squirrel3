using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip defaultAmbience;

    AudioSource music1, music2;

    private bool isPlayingtrack01;
    
    public static MusicManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {

        music1 = gameObject.AddComponent<AudioSource>();
        music2 = gameObject.AddComponent<AudioSource>();
        isPlayingtrack01 = true;

        SwapTrack(defaultAmbience);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapTrack(AudioClip newClip)
    {
        if (isPlayingtrack01)
        {
            music2.clip = newClip;
            music2.Play();
            music1.Stop();
        }
        else
        {
            music1.clip = newClip;
            music1.Play();
            music2.Stop();
        }
    }
}
