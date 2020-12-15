using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	AudioSource sound;
	public AudioClip pickupSound;

	void Start()
	{
		sound = GetComponent<AudioSource>();
	}

	public virtual void OnPickup()
	{
		sound.clip = pickupSound;
		sound.Play();
	}

	IEnumerator Disappear()
	{
		yield return new WaitForSeconds(0.125f);
		Destroy(this.gameObject);
	}
}
