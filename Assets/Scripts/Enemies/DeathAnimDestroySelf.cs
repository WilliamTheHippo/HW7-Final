using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimDestroySelf : MonoBehaviour
{
    protected AudioSource sound;
    public AudioClip dieSound;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.clip = dieSound;
        sound.Play();
        Destroy(this.gameObject, 0.8f);
    }
}