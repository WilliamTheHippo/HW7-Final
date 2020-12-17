﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Die : MonoBehaviour
{
    AudioSource sound;
    float ResetTimer = 3f;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.Play();
    }

    void Update()
    {   
        ResetTimer -= Time.deltaTime;
        Debug.Log(ResetTimer);
        if(ResetTimer <= 0f){
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
             Debug.Log("Scene Reset");
        }
    }
}

