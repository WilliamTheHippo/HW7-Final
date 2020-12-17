using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
	public AudioClip start;
	public AudioClip end;

	AudioSource sound;

	void Start()
	{
		sound = GetComponent<AudioSource>();
		sound.clip = start;
		sound.Play();
	}

	public void Switch()
	{
		sound.Stop();
		sound.clip = end;
		sound.Play();
	}
}
