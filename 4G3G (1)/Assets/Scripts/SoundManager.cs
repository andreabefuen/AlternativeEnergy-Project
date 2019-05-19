﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    public float volumen;

    public AudioClip hitButton;


    AudioSource audioSource;


    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHitButton()
    {
        audioSource.PlayOneShot(hitButton);
    }

    public void ChangeVolumen(float currentVolumen)
    {
        audioSource.volume = currentVolumen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
