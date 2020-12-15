using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Conditional
{
	protected Player player;

	public AudioClip pickupSound;

	bool pickedUp;

	void Start()
	{
		base.Start();
		pickedUp = false;
		sound = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Player" && !pickedUp)
		{
			pickedUp = true;
			player = c.GetComponent<Player>();
			OnPickup();
		}
	}

	public virtual void OnPickup()
	{
		sound.clip = pickupSound;
		sound.Play();
		StartCoroutine(PickupDisappear());
	}

	IEnumerator PickupDisappear()
	{
		yield return new WaitForSeconds(0.25f);
		Destroy(this.gameObject);
	}
}
