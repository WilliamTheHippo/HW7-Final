using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Die : MonoBehaviour
{
    AudioSource sound;
    float ResetTimer = 1.5f;

    void Start()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().Stop(); //hacky hacky
        sound = GetComponent<AudioSource>();
        sound.Play();
    }

    void Update()
    {   
        ResetTimer -= Time.deltaTime;

        if(ResetTimer <= 0f){
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }
}

