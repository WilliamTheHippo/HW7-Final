using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Fall : MonoBehaviour
{
    AudioSource sound;
    float ResetTimer = 0.8f;

    void Start()
    {
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
